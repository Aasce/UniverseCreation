using Asce.Game.Orbs;
using Asce.Managers;
using UnityEngine;

namespace Asce.Game.Enviroments
{
    public class EndGameLine : GameComponent
    {
        [SerializeField] private EdgeCollider2D _collider;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Orb")) return;
            if (!collision.gameObject.TryGetComponent(out Orb orb)) return;

            orb.IsValid = true;
        }
    }
}
