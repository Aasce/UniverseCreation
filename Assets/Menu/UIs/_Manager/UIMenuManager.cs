using Asce.Managers;
using Asce.Managers.Utils;
using Asce.Shared.UIs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asce.Menu.UIs
{
    public class UIMenuManager : MonoBehaviourSingleton<UIMenuManager>
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIMenuHUDController _hudController;
        [SerializeField] private UIPanelController _panelController;

        private readonly List<RaycastResult> _results = new();
        private PointerEventData _eventData;

        public Canvas Canvas => _canvas;
        public UIMenuHUDController HUDController => _hudController;
        public UIPanelController PanelController => _panelController;

        protected override void Reset()
        {
            base.Reset();
            this.LoadComponent(out _canvas);
            this.LoadComponent(out _hudController);
            this.LoadComponent(out _panelController);
        }
        /// <summary>
        ///     Returns true only if pointer is over a UI element in Screen Space canvas.
        /// </summary>
        public bool IsPointerOverScreenUI()
        {
            if (EventSystem.current == null) return false;
            if (!EventSystem.current.IsPointerOverGameObject()) return false;

            // Create once, reuse later
            if (_eventData == null) _eventData = new PointerEventData(EventSystem.current);

            // Always update pointer state
            _eventData.Reset(); // reset internal fields
            _eventData.position = Input.mousePosition;

            _results.Clear();
            EventSystem.current.RaycastAll(_eventData, _results);

            foreach (RaycastResult result in _results)
            {
                Canvas canvas = result.gameObject.GetComponentInParent<Canvas>();
                if (canvas != null && canvas.renderMode != RenderMode.WorldSpace)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
