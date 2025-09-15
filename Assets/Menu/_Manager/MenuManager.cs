using Asce.Managers;
using Asce.Shared;
using Asce.Shared.Audios;
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

        [Space]
        [SerializeField] private string _backgroundMusic = "Background";

        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
        }

        private void Start()
        {
            if (!AudioManager.Instance.IsPlayingMusic(_backgroundMusic))
            {
                AudioManager.Instance.PlayMusic(_backgroundMusic);
            }
        }

        public void PlayGame()
        {
            Shared.SharedData.isPlayAsNewGame = false;
            SceneLoader.Instance.Load(_mainGameScene, isShowLoadingScene: true, delay: _delay);
        }

        public void PlayNewGame()
        {
            Shared.SharedData.isPlayAsNewGame = true;
            SceneLoader.Instance.Load(_mainGameScene, isShowLoadingScene: true, delay: _delay);
        }

        public void Quit()
        {
            ProjectManager.Instance.Quit();
        }
    }
}