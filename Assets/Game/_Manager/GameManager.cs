using Asce.Game.Orbs;
using Asce.Game.Players;
using Asce.Game.SaveLoads;
using Asce.Game.Scores;
using Asce.Game.UIs;
using Asce.Managers;
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
            SaveLoadManager.Instance.LoadHistoryScore();
            if (Shared.SharedData.isPlayAsNewGame) NewGame();
            else StartGame();

            InvokeRepeating(nameof(AutoSave), _autoSaveInterval, _autoSaveInterval);
        }

        protected override void OnDestroy()
        {
            this.CancelInvoke();
            base.OnDestroy();
        }

        private void OnApplicationQuit()
        {
            SaveLoadManager.Instance.SaveCurrentGame();
        }

        public void EndGame()
        {
            CurrentGameState = GameState.GameOver;
            PlaytimeManager.Instance.StopTimer();
            ScoreManager.Instance.AddScoreToHistory();
            SaveLoadManager.Instance.SaveHistoryScores();

            UIGameOverPanel gameOver = UIGameManager.Instance.PanelController.GetPanel<UIGameOverPanel>();
            if (gameOver != null) gameOver.Show();
        }

        public void NewGame()
        {
            SaveLoadManager.Instance.DeleteCurrentGame();
            OrbManager.Instance.DespawnAll();
            ScoreManager.Instance.ResetScore();
            PlaytimeManager.Instance.StartTimer();
            Player.Instance.Dropper.ResetDropper();
            UIGameManager.Instance.HUDController.ResetHUD();

            CurrentGameState = GameState.Playing;
        }

        public void StartGame()
        {
            SaveLoadManager.Instance.LoadCurrentGame();
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
            SaveLoadManager.Instance.SaveCurrentGame();
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
                SaveLoadManager.Instance.DeleteCurrentGame();
                SaveLoadManager.Instance.SaveHistoryScores();
            }
            else SaveLoadManager.Instance.SaveCurrentGame();

            SceneLoader.Instance.Load(_menuScene, isShowLoadingScene: true, delay: _delay);
        }

        public void AutoSave()
        {
            // Save game
            SaveLoadManager.Instance.SaveCurrentGame();
        }
    }
}
