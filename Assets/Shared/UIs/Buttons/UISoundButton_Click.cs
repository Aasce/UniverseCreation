using Asce.Shared.Audios;
using UnityEngine;

namespace Asce.Shared.UIs
{
    public class UISoundButton_Click : UISoundButton
    {
        [SerializeField] private string _soundName = "Button Click";

        private void Start()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(Button_OnClick);
            }
        }

        private void Button_OnClick()
        {
            AudioManager.Instance.PlaySFX(_soundName);
        }
    }
}