using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Asce.Shared.UIs
{
    public class AnimationButton_HoverRotateIcon : AnimationButton, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _icon;

        [Space]
        [SerializeField] private float _rotation = 90f;
        [SerializeField] private float _zoomDuration = 0.1f;


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_icon == null) return;
            _icon.transform.DORotate(new Vector3(0f, 0f, _rotation), _zoomDuration).SetEase(Ease.OutSine);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_icon == null) return;
            _icon.transform.DORotate(Vector3.zero, _zoomDuration).SetEase(Ease.OutSine);
        }
    }
}
