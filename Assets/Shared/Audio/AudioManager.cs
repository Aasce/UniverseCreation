using Asce.Managers;
using Asce.Managers.Pools;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Asce.Shared.Audios
{
    public class AudioManager : DontDestroyOnLoadSingleton<AudioManager>
    {
        [SerializeField] private SO_Audios _audios;

        [Space]
        [SerializeField] private AudioSource _music;
        [SerializeField] private Pool<AudioSource> _sfxPool = new();

        [Header("Settings")]
        [SerializeField, Range(0f, 1f)] private float _masterVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _musicVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1f;

        public event Action<object, float> OnMasterVolumeChanged;
        public event Action<object, float> OnMusicVolumeChanged;
        public event Action<object, float> OnSFXVolumeChanged;

        public float MixedMusicVolume => _musicVolume * _masterVolume;
        public float MixedSFXVolume => _sfxVolume * _masterVolume;

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                if (Mathf.Approximately(_masterVolume, value)) return;
                _masterVolume = Mathf.Clamp01(value);
                this.ApplyMusicVolumes();
                this.ApplySFXVolumes();
                OnMasterVolumeChanged?.Invoke(this, _masterVolume);
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                if (Mathf.Approximately(_musicVolume, value)) return;
                _musicVolume = Mathf.Clamp01(value);
                this.ApplyMusicVolumes();
                OnMusicVolumeChanged?.Invoke(this, _musicVolume);
            }
        }

        public float SFXVolume
        {
            get => _sfxVolume;
            set
            {
                if (Mathf.Approximately(_sfxVolume, value)) return;
                _sfxVolume = Mathf.Clamp01(value);
                this.ApplySFXVolumes();
                OnSFXVolumeChanged?.Invoke(this, _sfxVolume);
            }
        }

        public SO_Audios Audios => _audios;
        public AudioSource MusicSource => _music;
        public Pool<AudioSource> SFXPool => _sfxPool;

        public void PlayMusic(string name, float fadeDuration = 0f)
        {
            if (_audios == null) return;
            if (_music == null) return;
            SO_AudioInformation info = _audios.Get(name);
            if (!info.IsValid()) return;

            if (fadeDuration > 0f && _music.isPlaying)
            {
                _music.DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    _music.Stop();
                    AudioExtension.Set(_music, info);
                    _music.volume = info.Volume * MixedMusicVolume;
                    _music.Play();
                });
            }
            else
            {
                AudioExtension.Set(_music, info);
                _music.volume = info.Volume * MixedMusicVolume;
                _music.Play();
            }
        }

        public void StopMusic(float fadeDuration = 0f)
        {
            if (_music == null) return;

            if (fadeDuration > 0f && _music.isPlaying)
            {
                _music.DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    _music.Stop();
                    _music.volume = 1f;
                });
            }
            else
            {
                _music.Stop();
                _music.volume = 1f;
            }
        }

        public bool IsPlayingMusic(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (_music == null) return false;
            if (_music.clip == null) return false;
            if (_music.isPlaying)
            {
                if (_music.name == name) return true;
            }
            return false;
        }

        public void PlaySFX(string name)
        {
            if (_audios == null) return;
            if (_sfxPool == null) return;

            AudioSource sfxSource = _sfxPool.Activate();
            if (sfxSource == null) return;
            SO_AudioInformation info = _audios.Get(name);

            AudioExtension.Set(sfxSource, info);
            sfxSource.volume = info.Volume * MixedSFXVolume;
            sfxSource.Play();

            StartCoroutine(DeactivateAfterPlay(sfxSource));
        }

        public void PlaySFX(string name, float delay) => StartCoroutine(this.PlaySFXDelay(name, delay));
        private IEnumerator PlaySFXDelay(string name, float delay)
        {
            yield return new WaitForSeconds(delay);
            this.PlaySFX(name);
        }

        private IEnumerator DeactivateAfterPlay(AudioSource source)
        {
            if (source == null || source.clip == null) yield break;
            float duration = (source.clip.loadState == AudioDataLoadState.Loaded) ? source.clip.length : 3f;
            yield return new WaitForSeconds(duration + 0.5f);

            source.Stop();
            source.clip = null;
            _sfxPool.Deactivate(source);
        }



        private void ApplyMusicVolumes()
        {
            if (_audios == null) return;
            if (_music == null || _music.clip == null) return;
            
            SO_AudioInformation info = _audios.Get(_music.name);
            if (info == null) return;
            
            _music.volume = info.Volume * MixedMusicVolume;
        }
        private void ApplySFXVolumes()
        {
            if (_audios == null) return;
            foreach (AudioSource sfx in _sfxPool.Activities)
            {
                if (sfx == null || sfx.clip == null) continue;
                SO_AudioInformation info = _audios.Get(sfx.name);

                if (info == null) continue;
                sfx.volume = info.Volume * MixedSFXVolume;
            }
        }

    }
}
