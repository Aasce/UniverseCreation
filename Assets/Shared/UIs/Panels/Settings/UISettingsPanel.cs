using Asce.Shared.Audios;
using Asce.Shared.SaveLoads;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Shared.UIs
{
    public abstract class UISettingsPanel : UIPanel
    {
        [Header("Content")]
        [SerializeField] protected Slider _masterVolume;
        [SerializeField] protected Slider _musicVolume;
        [SerializeField] protected Slider _sfxVolume;

        protected virtual void Start()
        {
            if (_masterVolume != null)
            {
                _masterVolume.value = AudioManager.Instance.MasterVolume;
                _masterVolume.onValueChanged.AddListener(MasterVolume_OnValueChanged);
            }

            if (_musicVolume != null)
            {
                _musicVolume.value = AudioManager.Instance.MusicVolume;
                _musicVolume.onValueChanged.AddListener(MusicVolume_OnValueChanged);
            }

            if (_sfxVolume != null)
            {
                _sfxVolume.value = AudioManager.Instance.SFXVolume;
                _sfxVolume.onValueChanged.AddListener(SFXVolume_OnValueChanged);
            }
        }


        public override void Hide()
        {
            if (!this.IsShow) return;

            ProjectSaveLoadManager.Instance.SaveSetttings();
            base.Hide();
        }


        private void MasterVolume_OnValueChanged(float value)
        {
            AudioManager.Instance.MasterVolume = value;
        }
        private void MusicVolume_OnValueChanged(float value)
        {
            AudioManager.Instance.MusicVolume = value;
        }
        private void SFXVolume_OnValueChanged(float value)
        {
            AudioManager.Instance.SFXVolume = value;
        }

    }
}
