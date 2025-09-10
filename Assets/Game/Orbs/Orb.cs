using Asce.Managers;
using Asce.Managers.Utils;
using UnityEngine;

namespace Asce.Game.Orbs
{
    public class Orb : GameComponent
    {
        [Header("References")]
        [SerializeField] private SO_OrbInformation _information;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("State")]
        [SerializeField] bool _isMerged = false;
        [SerializeField] private bool _isValid = false;

        public CircleCollider2D Collider => _collider;
        public Rigidbody2D Rigidbody => _rigidbody;

        public bool IsMerged
        {
            get => _isMerged;
            set => _isMerged = value;
        }

        public bool IsValid
        {
            get => _isValid;
            set => _isValid = value;
        }

        protected override void RefReset()
        {
            base.RefReset();
            this.LoadComponent(out _collider);
            this.LoadComponent(out _rigidbody);
        }


        public SO_OrbInformation Information => _information;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsMerged) return;
            if (collision.contactCount <= 0) return;
            if (!collision.gameObject.CompareTag(gameObject.tag)) return;
            if (!collision.gameObject.TryGetComponent(out Orb otherOrb)) return;

            OrbManager.Instance.Merge(this, otherOrb, collision.GetContact(0).point);
        }
    }
}