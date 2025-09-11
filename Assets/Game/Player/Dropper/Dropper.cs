using Asce.Game.Orbs;
using Asce.Game.Scores;
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
        [SerializeField] private Vector2 _torqueRange = new (-0.25f, 0.25f);
        [SerializeField] private Cooldown _dropCooldown = new(0.5f);

        [Space]
        [SerializeField, Range(1, 3)] private int _nextCount = 2;
        [SerializeField] private Vector2Int _nextRange = new(1, 3);
        private readonly Queue<int> _next = new();

        public event System.Action<object, Orb> OnCurrentOrbChanged;
        public event System.Action<object> OnDropped;


        public Orb CurrentOrb
        {
            get => _currentOrb;
            set
            {
                if (_currentOrb == value) return;
                _currentOrb = value;
                OnCurrentOrbChanged?.Invoke(this, _currentOrb);
            }
        }

        public Vector2 MoveRange => _moveRange;
        public Vector2 TorqueRange => _torqueRange;
        public Cooldown DropCooldown => _dropCooldown;

        public int NextCount => _nextCount;
        public Vector2 NextRange => _nextRange;
        public Queue<int> NextQueue => _next;


        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing) return;
            if (CurrentOrb.IsNull())
            {
                _dropCooldown.Update(Time.deltaTime);
                if (_dropCooldown.IsComplete)
                {
                    this.SpawnNewOrb();
                    _dropCooldown.Reset();
                }
            }
        }


        public void ResetDropper()
        {
            CurrentOrb = null;
            _dropCooldown.Reset();

            _next.Clear();
            while (_next.Count <= _nextCount)
            {
                _next.Enqueue(1);
            }

            this.SpawnNewOrb();
        }

        public void Move(float xPosition)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(xPosition, _moveRange.x, _moveRange.y);
            transform.position = newPosition;

            if (!CurrentOrb.IsNull())
            {
                CurrentOrb.transform.position = transform.position;
            }
        }

        public void Drop()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing) return;
            if (CurrentOrb.IsNull()) return;

            CurrentOrb.Rigidbody.simulated = true;
            float torque = Random.Range(_torqueRange.x, _torqueRange.y);
            CurrentOrb.Rigidbody.AddTorque(torque, ForceMode2D.Impulse);

            CurrentOrb.IsMerged = false;
            CurrentOrb = null;

            ScoreManager.Instance.AddDroppedScore();
            OnDropped?.Invoke(this);
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

            Orb newOrb = OrbManager.Instance.Spawn(level, transform.position);
            if (!newOrb.IsNull())
            {
                newOrb.Rigidbody.simulated = false;
                newOrb.IsMerged = true;

                CurrentOrb = newOrb;
            }
        }
    }
}
