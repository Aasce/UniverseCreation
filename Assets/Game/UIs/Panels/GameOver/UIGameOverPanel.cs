using Asce.Shared.UIs;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIGameOverPanel : UIPanel
    {
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
            Debug.Log("Show Ranking - Not Implemented Yet");
        }
    }
}
