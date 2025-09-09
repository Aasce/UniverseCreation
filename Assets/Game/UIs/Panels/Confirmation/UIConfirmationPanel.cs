using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIConfirmationPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _messageText;

        [Space]
        [SerializeField] private Button _yesButton;
        [SerializeField] private TextMeshProUGUI _yesButtonText;

        [Space]
        [SerializeField] private Button _noButton;
        [SerializeField] private TextMeshProUGUI _noButtonText;

        [Header("Config")]
        [SerializeField] private bool _hideOnStart = true;
        [SerializeField] private bool _resetOnHide = true;
        [SerializeField] private bool _hideOnButtonClicked = true;


        public bool HideOnStart { get => _hideOnStart; set => _hideOnStart = value; }
        public bool ResetOnHide { get => _resetOnHide; set => _resetOnHide = value; }
        public bool HideOnButtonClicked { get => _hideOnButtonClicked; set => _hideOnButtonClicked = value; }


        private void Start()
        {
            if (HideOnStart) this.Hide();
        }

        public override void Show()
        {
            transform.SetAsLastSibling();
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            if (ResetOnHide)
            {
                if (_yesButton != null) _yesButton.onClick.RemoveAllListeners();
                if (_noButton != null) _noButton.onClick.RemoveAllListeners();
                this.SetText("", "");
            }
        }

        public void SetDefault()
        {
            HideOnButtonClicked = true;
            ResetOnHide = true;
            this.SetYes();
            this.SetNo();
        }

        public void SetText(string title, string message)
        {
            this.SetTitle(title);
            this.SetMessage(message);
        }
        public void SetTitle(string title)
        {
            if (_titleText == null) return;
            _titleText.gameObject.SetActive(!string.IsNullOrEmpty(title));
            _titleText.text = title;
        }
        public void SetMessage(string message)
        {
            if (_messageText == null) return;
            _messageText.gameObject.SetActive(!string.IsNullOrEmpty(message));
            _messageText.text = message;
        }


        public void HideYes() => SetYes("", null);
        public void SetYes() => SetYes("Yes", null);
        public void SetYes(string text) => SetYes(text, null);
        public void SetYes(UnityAction yesAction) => SetYes("Yes", yesAction);
        public void SetYes(string text, UnityAction yesAction)
        {
            if (_yesButton == null) return;
            _yesButton.gameObject.SetActive(!string.IsNullOrEmpty(text));

            if (_yesButtonText != null) _yesButtonText.text = text;
            if (yesAction != null) _yesButton.onClick.AddListener(yesAction);
            if (HideOnButtonClicked) _yesButton.onClick.AddListener(Hide);
        }

        public void HideNo() => SetNo("", null);
        public void SetNo() => SetNo("No", null);
        public void SetNo(string text) => SetNo(text, null);
        public void SetNo(UnityAction noAction) => SetNo("No", noAction);
        public void SetNo(string text, UnityAction noAction)
        {
            if (_noButton == null) return;
            _noButton.gameObject.SetActive(!string.IsNullOrEmpty(text));

            if (_noButtonText != null) _noButtonText.text = text;
            if (noAction != null) _noButton.onClick.AddListener(noAction);
            if (HideOnButtonClicked) _noButton.onClick.AddListener(Hide);
        }
    }
}
