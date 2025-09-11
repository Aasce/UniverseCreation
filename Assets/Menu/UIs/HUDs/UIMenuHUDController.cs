using Asce.Managers.UIs;
using Asce.Shared.UIs;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Menu.UIs
{
    public class UIMenuHUDController : UIObject
    {
        [Header("Control Buttons")]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _infoButton;

        [Header("Action Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _quitButton;

        [Space]
        [SerializeField] private UICopyright _copyright;

        private void Start()
        {
            if (_settingsButton != null) _settingsButton.onClick.AddListener(SettingsButton_OnClick);
            if (_infoButton != null) _infoButton.onClick.AddListener(InfoButton_OnClick);

            if (_playButton != null) _playButton.onClick.AddListener(PlayButton_OnClick);
            if (_newGameButton != null) _newGameButton.onClick.AddListener(NewGameButton_OnClick);
            if (_quitButton != null) _quitButton.onClick.AddListener(QuitButton_OnClick);
        }


        private void SettingsButton_OnClick()
        {
            UISettingsPanel settings = UIMenuManager.Instance.PanelController.GetPanel<UISettingsPanel>();
            if (settings != null) settings.Show();
        }
        private void InfoButton_OnClick()
        {

        }

        private void PlayButton_OnClick()
        {
            MenuManager.Instance.PlayGame();
        }

        private void NewGameButton_OnClick()
        {
            UIConfirmationPanel confirmation = UIMenuManager.Instance.PanelController.GetPanel<UIConfirmationPanel>();
            if (confirmation == null) MenuManager.Instance.PlayNewGame();
            else
            {
                confirmation.SetDefault();
                confirmation.SetText("New Game", "Are you sure you want to start a new game?\nUnsaved progress will be lost.");
                confirmation.SetYes(() =>
                {
                    MenuManager.Instance.PlayNewGame();
                });
                confirmation.Show();
            }

        }

        private void QuitButton_OnClick()
        {
            UIConfirmationPanel confirmation = UIMenuManager.Instance.PanelController.GetPanel<UIConfirmationPanel>();
            if (confirmation == null) MenuManager.Instance.Quit();
            else
            {
                confirmation.SetDefault();
                confirmation.SetText("Quit Game", "Are you sure you want to quit the game?");
                confirmation.SetYes(() =>
                {
                    MenuManager.Instance.Quit();
                });
                confirmation.Show();
            }
        }

    }
}
