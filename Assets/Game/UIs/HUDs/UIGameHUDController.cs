using Asce.Game.Scores;
using Asce.Managers.UIs;
using Asce.Managers.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIGameHUDController : UIObject
    {
        [Header("Top UI")]
        [SerializeField] private UIScore _bestScore;
        [SerializeField] private UIScore _currentScore;

        [Space]
        [SerializeField] private UIPlaytime _playtime;
        [SerializeField] private List<UINextOrb> _nextOrbs = new();

        [Header("Bottom UI")]
        [SerializeField] private Button _pauseButton;
        [SerializeField] private UICancelDrop _cancelDrop;


        public UIScore BestScore => _bestScore;
        public UIScore CurrentScore => _currentScore;

        public UIPlaytime Playtime => _playtime;
        public List<UINextOrb> NextOrbs => _nextOrbs;

        public Button PauseButton => _pauseButton;
        public UICancelDrop CancelDrop => _cancelDrop;

        protected override void RefReset()
        {
            base.RefReset();
            this.LoadComponent(out _playtime);
            this.LoadComponent(out _cancelDrop);
        }

        private void Start()
        {
            if (_pauseButton != null)
                _pauseButton.onClick.AddListener(PauseButton_OnClicked);

            if (_bestScore != null)
            {
                _bestScore.SetScore(ScoreManager.Instance.BestScore);
                ScoreManager.Instance.OnBestScoreChanged += ScoreManager_OnBestScoreChanged;
            }

            if (_currentScore != null)
            {
                _currentScore.SetScore(ScoreManager.Instance.CurrentScore);
                ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
            }
        }

        public void ResetHUD()
        {
            if (_currentScore != null) _currentScore.SetScore(ScoreManager.Instance.CurrentScore);

            foreach (UINextOrb nextOrb in _nextOrbs)
            {
                if (nextOrb != null) nextOrb.ResetNextOrb();
            }
        }


        private void PauseButton_OnClicked()
        {
            UIGameSettingsPanel settings = UIGameManager.Instance.PanelController.GetPanel<UIGameSettingsPanel>();
            if (settings != null)
            {
                settings.Show();
                GameManager.Instance.PauseGame();
            }
        }

        private void ScoreManager_OnBestScoreChanged(object sender, int newScore)
        {
            _bestScore.SetScore(newScore);
        }

        private void ScoreManager_OnScoreChanged(object sender, int newScore)
        {
            _currentScore.SetScore(newScore);
        }

    }
}
