using Asce.Game.Orbs;
using Asce.Game.Players;
using Asce.Game.SaveLoads;
using Asce.Game.Scores;
using Asce.Game.UIs;
using Asce.Managers;
using Asce.Shared.Audios;
using System;
using UnityEngine;

namespace Asce.Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        // [Header("Game Settings")]
        // [SerializeField] private int _targetFrameRate = 60;

        [Header("References")]
        [SerializeField] private Camera _mainCamera;

        [Header("State")]
        [SerializeField] private GameState _gameState = GameState.Init;

        [Header("Auto Save")]
        [SerializeField] private float _autoSaveInterval = 10f;

        [Header("Settings")]
        [SerializeField] private string _menuScene = "Menu";
        [SerializeField] private float _delay = 0f;

        [Space]
        [SerializeField] private string _backgroundMusic = "Background";
        [SerializeField] private string _gameOverSFX = "Game Over";
        [SerializeField, Min(0f)] private float _fadeDelay = 0.5f;


        public event Action<object, GameState> OnGameStateChanged;

        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
        }


        public GameState CurrentGameState
        {
            get => _gameState;
            set
            {
                if (_gameState == value) return;
                _gameState = value;
                OnGameStateChanged?.Invoke(this, _gameState);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            // Application.targetFrameRate = _targetFrameRate;
        }

        private void Start()
        {
            GameSaveLoadManager.Instance.LoadHistoryScore();
            if (Shared.SharedData.isPlayAsNewGame) NewGame();
            else StartGame();

            if (!AudioManager.Instance.IsPlayingMusic(_backgroundMusic))
            {
                AudioManager.Instance.PlayMusic(_backgroundMusic);
            }

            InvokeRepeating(nameof(AutoSave), _autoSaveInterval, _autoSaveInterval);
        }

        protected override void OnDestroy()
        {
            this.CancelInvoke();
            base.OnDestroy();
        }

        private void OnApplicationQuit()
        {
            GameSaveLoadManager.Instance.SaveCurrentGame();
        }

        public void EndGame()
        {
            CurrentGameState = GameState.GameOver;
            PlaytimeManager.Instance.StopTimer();
            ScoreManager.Instance.AddScoreToHistory();
            GameSaveLoadManager.Instance.SaveHistoryScores();

            AudioManager.Instance.StopMusic(fadeDuration: _fadeDelay);
            AudioManager.Instance.PlaySFX(_gameOverSFX, delay: _fadeDelay);

            UIGameOverPanel gameOver = UIGameManager.Instance.PanelController.GetPanel<UIGameOverPanel>();
            if (gameOver != null) gameOver.Show();
        }

        public void NewGame()
        {
            GameSaveLoadManager.Instance.DeleteCurrentGame();
            OrbManager.Instance.DespawnAll();
            ScoreManager.Instance.ResetScore();
            PlaytimeManager.Instance.StartTimer();
            Player.Instance.Dropper.ResetDropper();
            UIGameManager.Instance.HUDController.ResetHUD();

            if (!AudioManager.Instance.IsPlayingMusic(_backgroundMusic))
            {
                AudioManager.Instance.PlayMusic(_backgroundMusic);
            }

            CurrentGameState = GameState.Playing;
        }

        public void StartGame()
        {
            GameSaveLoadManager.Instance.LoadCurrentGame();
            UIGameManager.Instance.HUDController.ResetHUD();

            CurrentGameState = GameState.Playing;
        }

        public void PauseGame()
        {
            if (CurrentGameState == GameState.Playing)
            {
                CurrentGameState = GameState.Paused;
                PlaytimeManager.Instance.StopTimer();
            }
        }

        public void ResumeGame()
        {
            // Save game
            GameSaveLoadManager.Instance.SaveCurrentGame();
            if (CurrentGameState == GameState.Paused)
            {
                CurrentGameState = GameState.Playing;
                PlaytimeManager.Instance.ResumeTimer();
            }
        }

        public void BackToMenu()
        {
            // Save game
            if (CurrentGameState == GameState.GameOver)
            {
                GameSaveLoadManager.Instance.DeleteCurrentGame();
                GameSaveLoadManager.Instance.SaveHistoryScores();
            }
            else GameSaveLoadManager.Instance.SaveCurrentGame();

            SceneLoader.Instance.Load(_menuScene, isShowLoadingScene: true, delay: _delay);
        }

        public void AutoSave()
        {
            // Save game
            GameSaveLoadManager.Instance.SaveCurrentGame();
        }
    }
}
