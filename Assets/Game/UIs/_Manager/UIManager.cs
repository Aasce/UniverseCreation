using Asce.Managers;
using Asce.Managers.Utils;
using UnityEngine;

namespace Asce.Game.UIs
{
    public class UIManager : MonoBehaviourSingleton<UIManager>
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIHUDController _hudController;
        [SerializeField] private UIPanelController _panelController;

        public Canvas Canvas => _canvas;
        public UIHUDController HUDController => _hudController;
        public UIPanelController PanelController => _panelController;

        protected override void Reset()
        {
            base.Reset();
            this.LoadComponent(out _canvas);
            this.LoadComponent(out _hudController);
            this.LoadComponent(out _panelController);
        }
    }
}
