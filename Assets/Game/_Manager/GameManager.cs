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
            Debug.Log("Game Over!");
            CurrentGameState = GameState.GameOver;
            // Implement additional game over logic here (e.g., show game over screen, reset game, etc.)
        }

    }
}
