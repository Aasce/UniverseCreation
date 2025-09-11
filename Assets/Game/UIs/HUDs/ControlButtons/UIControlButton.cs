using Asce.Managers.UIs;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIControlButton : UIObject
    {
        [SerializeField] private Image _icon;

        [Header("Icons")]
        [SerializeField] private Sprite _playIcon;
        [SerializeField] private Sprite _pauseIcon;
        [SerializeField] private Sprite _gameOverIcon;

        public Image Icon => _icon;


        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        }

        private void GameManager_OnGameStateChanged(object sender, GameState state)
        {
            if (Icon == null) return;
            switch (state)
            {
                case GameState.Playing:
                    Icon.sprite = _pauseIcon;
                    break;

                case GameState.Paused:
                    Icon.sprite = _playIcon;
                    break;

                case GameState.GameOver:
                    Icon.sprite = _gameOverIcon;
                    break;

                default:
                    break;
            }
        }
    }
}
