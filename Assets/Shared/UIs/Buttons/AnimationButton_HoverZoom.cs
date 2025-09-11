using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asce.Shared.UIs
{
    public class AnimationButton_HoverZoom : AnimationButton, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _zoomScale = 1.1f;
        [SerializeField] private float _zoomDuration = 0.1f;


        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_zoomScale, _zoomDuration).SetEase(Ease.OutSine).SetLink(gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, _zoomDuration).SetEase(Ease.OutSine).SetLink(gameObject);
        }
    }

}