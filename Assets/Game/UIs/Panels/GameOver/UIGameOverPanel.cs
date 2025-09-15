using Asce.Game.Orbs;
using Asce.Game.Players;
using Asce.Shared.UIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIGameOverPanel : UIPanel
    {
        [Header("Result")]
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _playtime;
        [SerializeField] private TextMeshProUGUI _droppedCount;
        [SerializeField] private TextMeshProUGUI _mergedCount;

        [Header("Action Buttons")]
        [SerializeField] private Button _backMenuButton;
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _rankingButton;

        private void Start()
        {
            if (_backMenuButton != null)
                _backMenuButton.onClick.AddListener(BackMenuButton_OnClicked);

            if (_tryAgainButton != null)
                _tryAgainButton.onClick.AddListener(TryAgainButton_OnClicked);

            if (_rankingButton != null)
                _rankingButton.onClick.AddListener(RankingButton_OnClicked);
        }

        public override void Show()
        {
            if (this.IsShow) return;
            if (_score != null) _score.text = Scores.ScoreManager.Instance.CurrentScore.ToString();
            if (_playtime != null) _playtime.text = PlaytimeManager.Instance.GetPlaytimeAsText();
            if (_droppedCount != null) _droppedCount.text = Player.Instance.Dropper.DropCount.ToString();
            if (_mergedCount != null) _mergedCount.text = OrbManager.Instance.MergedCount.ToString();
            base.Show();
        }

        private void BackMenuButton_OnClicked()
        {
            UIConfirmationPanel confirmation = UIGameManager.Instance.PanelController.GetPanel<UIConfirmationPanel>();
            if (confirmation != null)
            {
                confirmation.SetDefault();
                confirmation.SetText("Back to Main Menu", "Are you sure you want to go back to the main menu? progress will be save.");
                confirmation.SetYes(() =>
                {
                    GameManager.Instance.BackToMenu();
                    this.Hide();
                });
                confirmation.Show();
            }
        }

        private void TryAgainButton_OnClicked()
        {
            GameManager.Instance.NewGame();
            this.Hide();
        }

        private void RankingButton_OnClicked()
        {
            UIRankings rankings = UIGameManager.Instance.PanelController.GetPanel<UIRankings>();
            if (rankings != null) rankings.Show();
        }
    }
}
