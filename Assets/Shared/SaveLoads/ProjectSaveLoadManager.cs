using Asce.Managers;
using Asce.Managers.SaveLoads;
using Asce.Shared.Audios;
using UnityEngine;

namespace Asce.Shared.SaveLoads
{
    public class ProjectSaveLoadManager : DontDestroyOnLoadSingleton<ProjectSaveLoadManager>
    {
        [SerializeField] private string _settingsFile = "player/settings.json";

        public void SaveSetttings()
        {
            SettingsData settingsData = new();
            settingsData.masterVolume = AudioManager.Instance.MasterVolume;
            settingsData.musicVolume = AudioManager.Instance.MusicVolume;
            settingsData.sfxVolume = AudioManager.Instance.SFXVolume;
            SaveLoadSystem.Save(settingsData, _settingsFile);
        }

        public void LoadSetttings()
        {
            SettingsData settingsData = SaveLoadSystem.Load<SettingsData>(_settingsFile);
            AudioManager.Instance.MasterVolume = settingsData?.masterVolume ?? 0.75f;
            AudioManager.Instance.MusicVolume = settingsData?.musicVolume ?? 0.75f;
            AudioManager.Instance.SFXVolume = settingsData?.sfxVolume ?? 0.75f;

        }

        public void DeleteSettings() => SaveLoadSystem.Delete(_settingsFile);
    }
}