using Asce.Managers.UIs;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIHUDController : UIObject
    {
        [SerializeField] private Button _pauseButton;


        private void Start()
        {
            if (_pauseButton != null)
                _pauseButton.onClick.AddListener(PauseButton_OnClicked);
        }

        private void PauseButton_OnClicked()
        {
            UISettingsPanel settings = UIManager.Instance.PanelController.GetPanel<UISettingsPanel>();
            if (settings != null)
            {
                settings.Show();
                GameManager.Instance.PauseGame();
            }
        }
    }
}
