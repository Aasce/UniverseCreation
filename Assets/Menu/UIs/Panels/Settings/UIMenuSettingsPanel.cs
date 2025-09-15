using Asce.Shared.UIs;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Menu.UIs
{
    public class UIMenuSettingsPanel : UISettingsPanel
    {
        [Header("Action Buttons")]
        [SerializeField] private Button _closeButton;


        protected override void Start()
        {
            base.Start();
            if (_closeButton != null) _closeButton.onClick.AddListener(this.Hide);
        }
    }
}
