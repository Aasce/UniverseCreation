using Asce.Game.Orbs;
using Asce.Managers;
using UnityEngine;

namespace Asce.Game
{
    public class BorderLine : GameComponent
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Orb")) return;
            if (!collision.gameObject.TryGetComponent(out Orb orb)) return;

            OrbManager.Instance.Despawn(orb);
        }
    }
}
