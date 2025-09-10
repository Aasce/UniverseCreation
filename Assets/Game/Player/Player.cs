using Asce.Game.Orbs;
using Asce.Game.UIs;
using Asce.Managers;
using UnityEngine;

namespace Asce.Game.Players
{
    public class Player : MonoBehaviourSingleton<Player>
    {
        [SerializeField] private Dropper _dropper;
        [SerializeField] private bool _canDrop = false;

        public Dropper Dropper => _dropper;


        private void Update()
        {
            this.ControlDropper();
        }

        private void ControlDropper()
        {
            if (_dropper == null) return;
            if (GameManager.Instance == null) return;
            if (GameManager.Instance.MainCamera == null) return;
            if (GameManager.Instance.CurrentGameState != GameState.Playing) return;

            Vector2 pointer = GameManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            _dropper.Move(pointer.x);

            bool isPointerOverUI = UIManager.Instance.IsPointerOverScreenUI();
            if (Input.GetMouseButtonDown(0))
            {
                if (isPointerOverUI)_canDrop = false;
                else _canDrop = true;
            }

            if (Input.GetMouseButton(0))
            {
                if (!_dropper.CurrentOrb.IsNull())
                {
                    if (!isPointerOverUI) 
                        UIManager.Instance.HUDController.CancelDrop.Show();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_canDrop && !isPointerOverUI)
                {
                    _dropper.Drop();
                    _canDrop = false;
                }
                UIManager.Instance.HUDController.CancelDrop.Hide();
            }

        }
    }
}
