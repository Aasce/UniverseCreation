using Asce.Managers;
using System;
using UnityEngine;

public class ProjectManager : DontDestroyOnLoadSingleton<ProjectManager>
{
    public event Action<object> OnBeforeQuit;

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
