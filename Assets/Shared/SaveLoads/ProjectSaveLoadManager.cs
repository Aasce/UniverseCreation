using Asce.Managers;
using Asce.Managers.SaveLoads;
using Asce.Shared.Audios;
using UnityEngine;

namespace Asce.Shared.SaveLoads
{
    public class ProjectSaveLoadManager : DontDestroyOnLoadSingleton<ProjectSaveLoadManager>
    {
        [SerializeField] private string _volumeSettingsFile = "player/volume_settings.json";

        public void SaveSetttings()
        {
            if (AudioManager.Instance == null) return;
            SettingsData settingsData = new()
            {
                masterVolume = AudioManager.Instance.MasterVolume,
                musicVolume = AudioManager.Instance.MusicVolume,
                sfxVolume = AudioManager.Instance.SFXVolume
            };
            SaveLoadSystem.Save(settingsData, _volumeSettingsFile);
        }

        public void LoadSetttings()
        {
            if (AudioManager.Instance == null) return;
            SettingsData settingsData = SaveLoadSystem.Load<SettingsData>(_volumeSettingsFile);
            AudioManager.Instance.MasterVolume = settingsData?.masterVolume ?? 0.75f;
            AudioManager.Instance.MusicVolume = settingsData?.musicVolume ?? 0.75f;
            AudioManager.Instance.SFXVolume = settingsData?.sfxVolume ?? 0.75f;
        }

        public void DeleteSettings() => SaveLoadSystem.Delete(_volumeSettingsFile);
    }
}