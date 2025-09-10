using Asce.Managers.UIs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asce.Game.UIs
{
    public class UICancelDrop : UIObject, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool _isCancel = false;

        public bool IsCancel => _isCancel;

        private void Start()
        {
            this.Hide();
        }

        public override void Hide()
        {
            base.Hide();
            _isCancel = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isCancel = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isCancel = false;
        }

    }
}
