using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UISettingsPanel : UIPanel
    {
        [Header("Action Buttons")]
        [SerializeField] private Button _backMenuButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _resumeButton;


        private void Start()
        {
            if (_backMenuButton != null)
                _backMenuButton.onClick.AddListener(BackMenuButton_OnClicked);

            if (_newGameButton != null)
                _newGameButton.onClick.AddListener(NewGameButton_OnClicked);

            if (_resumeButton != null)
                _resumeButton.onClick.AddListener(ResumeButton_OnClicked);
        }

        private void BackMenuButton_OnClicked()
        {
            UIConfirmationPanel confirmation = UIManager.Instance.PanelController.GetPanel<UIConfirmationPanel>();
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

        private void NewGameButton_OnClicked()
        {
            UIConfirmationPanel confirmation = UIManager.Instance.PanelController.GetPanel<UIConfirmationPanel>();
            if (confirmation != null)
            {
                confirmation.SetDefault();
                confirmation.SetText("Start New Game", "Are you sure you want to start a new game? Unsaved progress will be lost.");
                confirmation.SetYes(() =>
                {
                    GameManager.Instance.NewGame();
                    this.Hide();
                });
                confirmation.Show();
            }
        }

        private void ResumeButton_OnClicked()
        {
            GameManager.Instance.ResumeGame();
            this.Hide();
        }
    }
}
