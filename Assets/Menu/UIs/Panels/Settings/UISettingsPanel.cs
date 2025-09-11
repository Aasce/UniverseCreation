using Asce.Shared.UIs;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Menu.UIs
{
    public class UISettingsPanel : UIPanel
    {
        [SerializeField] private Button _closeButton;


        private void Start()
        {
            if (_closeButton != null) _closeButton.onClick.AddListener(this.Hide);
        }
    }
}
