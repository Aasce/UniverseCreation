using Asce.Game.Orbs;
using Asce.Game.Players;
using Asce.Managers.UIs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Asce.Game.UIs
{
    public class UINextOrb : UIObject
    {
        [SerializeField] private Image _iconImage;

        [Space]
        [SerializeField, Min(0)] private int _nextOrbIndex = 0;

        [Header("Move Settings")]
        [SerializeField] private Transform _moveToTransform;
        [SerializeField] private bool _isUITransform = true;
        [SerializeField, Min(0f)] private float _moveDuration = 0.5f;   // Time to move
        [SerializeField, Min(0f)] private float _resetDelay = 0.2f;     // Delay before reset

        private Vector3 _startLocalPosition;
        private Coroutine _moveCoroutine;

        private void Awake()
        {
            if (_iconImage != null)
                _startLocalPosition = _iconImage.rectTransform.localPosition;
        }

        private void Start()
        {
            if (Player.Instance == null) return;
            if (Player.Instance.Dropper == null) return;
            Player.Instance.Dropper.OnDropped += Dropper_OnDropped;
        }

        public void ResetNextOrb()
        {
            if (Player.Instance == null) return;
            if (Player.Instance.Dropper == null) return;

            Queue<int> nextQueue = Player.Instance.Dropper.NextQueue;
            if (nextQueue == null) return;
            if (_nextOrbIndex < 0 || _nextOrbIndex >= nextQueue.Count) return;

            int orbLevel = nextQueue.ElementAt(_nextOrbIndex);
            Orb orbPrefab = OrbManager.Instance.OrbsData.GetOrbPrefab(orbLevel);
            if (orbPrefab.IsNull()) return;

            if (_iconImage != null) _iconImage.rectTransform.localPosition = _startLocalPosition;
            SetIcon(orbPrefab.Information.Icon);
        }


        private void Dropper_OnDropped(object sender)
        {
            Queue<int> nextQueue = Player.Instance.Dropper.NextQueue;
            if (nextQueue == null) return;
            if (_nextOrbIndex < 0 || _nextOrbIndex >= nextQueue.Count) return;

            int orbLevel = nextQueue.ElementAt(_nextOrbIndex);
            Orb orbPrefab = OrbManager.Instance.OrbsData.GetOrbPrefab(orbLevel);
            if (orbPrefab.IsNull()) return;

            // Start move animation
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveAndResetIcon(orbPrefab.Information.Icon));
        }

        private IEnumerator MoveAndResetIcon(Sprite newIcon)
        {
            if (_iconImage == null || _moveToTransform == null)
                yield break;

            RectTransform iconRect = _iconImage.rectTransform;
            RectTransform parentRect = iconRect.parent as RectTransform;
            if (parentRect == null)
                parentRect = UIManager.Instance.Canvas.transform as RectTransform;

            Canvas canvas = UIManager.Instance.Canvas;
            Camera canvasCamera = (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                ? GameManager.Instance.MainCamera
                : null;

            Vector3 startLocal = iconRect.localPosition;

            float elapsed = 0f;
            while (elapsed < _moveDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _moveDuration);

                // Recalculate target position every frame
                Vector3 targetLocal = startLocal;

                if (_isUITransform)
                {
                    Vector3 targetWorldPos = _moveToTransform.position;
                    targetLocal = parentRect.InverseTransformPoint(targetWorldPos);
                    targetLocal.z = startLocal.z;
                }
                else
                {
                    Vector3 screenPos = GameManager.Instance.MainCamera.WorldToScreenPoint(_moveToTransform.position);
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        parentRect,
                        screenPos,
                        canvasCamera,
                        out Vector2 localPoint
                    );
                    targetLocal = new Vector3(localPoint.x, localPoint.y, startLocal.z);
                }

                // Smoothly follow the moving target
                iconRect.localPosition = Vector3.Lerp(startLocal, targetLocal, t);

                yield return null;
            }

            // Ensure final snap
            {
                Vector3 finalTarget = _isUITransform
                    ? parentRect.InverseTransformPoint(_moveToTransform.position)
                    : (Vector3)(RectTransformUtility.WorldToScreenPoint(GameManager.Instance.MainCamera, _moveToTransform.position));

                iconRect.localPosition = finalTarget;
            }

            yield return new WaitForSeconds(_resetDelay);

            iconRect.localPosition = _startLocalPosition;
            SetIcon(newIcon);
            _moveCoroutine = null;
        }


        private void SetIcon(Sprite icon)
        {
            if (_iconImage == null) return;
            _iconImage.sprite = icon;
        }
    }
}
