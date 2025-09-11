using Asce.Managers.UIs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asce.Game.UIs
{
    public class UICancelDrop : UIObject, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Settings")]
        [SerializeField] private float _zoomScale = 1.05f;
        [SerializeField] private float _zoomDuration = 0.1f;

        [Space]
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
            transform.DOScale(_zoomScale, _zoomDuration).SetEase(Ease.OutSine);
            _isCancel = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, _zoomDuration).SetEase(Ease.OutSine);
            _isCancel = false;
        }

    }
}
