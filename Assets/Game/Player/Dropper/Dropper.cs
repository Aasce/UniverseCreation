using Asce.Game.Orbs;
using Asce.Managers;
using Asce.Managers.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.Players
{
    public class Dropper : GameComponent
    {
        [SerializeField] private Orb _currentOrb;

        [Space]
        [SerializeField] private Vector2 _moveRange = new (-2f, 2f);
        [SerializeField] private Cooldown _dropCooldown = new(0.5f);

        [Space]
        [SerializeField, Range(1, 3)] private int _nextCount = 2;
        [SerializeField] private Vector2Int _nextRange = new(1, 3);
        private readonly Queue<int> _next = new();

        public Vector2 MoveRange => _moveRange;
        public Cooldown DropCooldown => _dropCooldown;

        public int NextCount => _nextCount;
        public Vector2 NextRange => _nextRange;
        public Queue<int> NextQueue => _next;


        private void Start()
        {
            // Initialize the next queue with random levels
            while (_next.Count < _nextCount)
            {
                this.AddNext();
            }

            this.SpawnNewOrb();
        }

        private void Update()
        {
            if (_currentOrb.IsNull())
            {
                _dropCooldown.Update(Time.deltaTime);
                if (_dropCooldown.IsComplete)
                {
                    this.SpawnNewOrb();
                    _dropCooldown.Reset();
                }
            }

            if (!_currentOrb.IsNull())
            {
                _currentOrb.transform.position = transform.position;
            }
        }

        public void Move(float xPosition)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(xPosition, _moveRange.x, _moveRange.y);
            transform.position = newPosition;
        }

        public void Drop()
        {
            if (_currentOrb.IsNull()) return;
            
            _currentOrb.Rigidbody.simulated = true;
            _currentOrb.IsMerged = false;
            _currentOrb = null;
        }

        private void AddNext()
        {
            if (_next.Count >= _nextCount) return;
            int newLevel = Random.Range(_nextRange.x, _nextRange.y + 1);
            _next.Enqueue(newLevel);
        }

        private void SpawnNewOrb()
        {
            if (_next.Count <= 0) return;
            int level = _next.Dequeue();
            this.AddNext();

            _currentOrb = OrbManager.Instance.Spawn(level, transform.position);
            if (!_currentOrb.IsNull())
            {
                _currentOrb.Rigidbody.simulated = false;
                _currentOrb.IsMerged = true;
            }
        }
    }
}
