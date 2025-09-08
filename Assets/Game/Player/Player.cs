using Asce.Managers;
using UnityEngine;

namespace Asce.Game.Players
{
    public class Player : MonoBehaviourSingleton<Player>
    {
        [SerializeField] private Dropper _dropper;

        private void Update()
        {
            this.ControlDropper();
        }

        private void ControlDropper()
        {
            if (_dropper == null) return;
            if (GameManager.Instance == null) return;
            if (GameManager.Instance.MainCamera == null) return;

            Vector2 pointer = GameManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);

            _dropper.Move(pointer.x);
            if (Input.GetMouseButtonUp(0))
            {
                _dropper.Drop();
            }
        }
    }
}
