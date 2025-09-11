using Asce.Game.Orbs;
using Asce.Game.Players;
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
            CurrentGameState = GameState.Playing;
        }

        public void EndGame()
        {
            CurrentGameState = GameState.GameOver;
            UIGameOverPanel gameOver = UIGameManager.Instance.PanelController.GetPanel<UIGameOverPanel>();
            if (gameOver != null) gameOver.Show();
        }

        public void NewGame()
        {
            OrbManager.Instance.DespawnAll();
            ScoreManager.Instance.ResetScore();
            Player.Instance.Dropper.ResetDropper();
            UIGameManager.Instance.HUDController.ResetHUD();

            CurrentGameState = GameState.Playing;
        }

        public void PauseGame()
        {
            if (CurrentGameState == GameState.Playing)
            {
                CurrentGameState = GameState.Paused;
            }
        }

        public void ResumeGame()
        {
            if (CurrentGameState == GameState.Paused)
            {
                CurrentGameState = GameState.Playing;
            }
        }

        public void BackToMenu()
        {
            // Save game
            SceneLoader.Instance.Load(_menuScene, isShowLoadingScene: true, delay: _delay);
        }
    }
}
