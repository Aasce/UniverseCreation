using Asce.Managers;
using Asce.Shared.SaveLoads;
using System;
using UnityEngine;

namespace Asce.Shared
{
    public class ProjectManager : DontDestroyOnLoadSingleton<ProjectManager>
    {
        public event Action<object> OnBeforeQuit;

        private void Start()
        {
            ProjectSaveLoadManager.Instance.LoadSetttings();
        }

        protected override void OnApplicationQuit()
        {
            if (ProjectSaveLoadManager.Instance != null) 
                ProjectSaveLoadManager.Instance.SaveSetttings();

            base.OnApplicationQuit();
        }

        public void Quit()
        {
            OnBeforeQuit?.Invoke(this);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
    }
}