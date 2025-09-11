using Asce.Managers;
using UnityEngine;

namespace Asce.Menu
{
    public class MenuManager : MonoBehaviourSingleton<MenuManager>
    {
        [Header("References")]
        [SerializeField] private Camera _mainCamera;

        [Header("Settings")]
        [SerializeField] private string _mainGameScene = "MainGame";
        [SerializeField] private float _delay = 0f;

        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
        }

        public void PlayGame()
        {
            SceneLoader.Instance.Load(_mainGameScene, isShowLoadingScene: true, delay: _delay);
        }

        public void PlayNewGame()
        {
            SceneLoader.Instance.Load(_mainGameScene, isShowLoadingScene: true, delay: _delay);
        }

        public void Quit()
        {
            ProjectManager.Instance.Quit();
        }
    }
}