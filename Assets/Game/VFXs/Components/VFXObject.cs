using Asce.Managers;
using Asce.Managers.Utils;
using UnityEngine;

namespace Asce.Game.VFXs
{
    public class VFXObject : GameComponent
    {
        [SerializeField] private Cooldown _despawnCooldown = new(0f);

        public event System.Action<object> OnCompleted;

        public Cooldown DespawnCooldown => _despawnCooldown;

        protected virtual void Update()
        {
            _despawnCooldown.Update(Time.deltaTime);
            if (_despawnCooldown.IsComplete)
            {
                this.Stop();
            }
        }

        public virtual void Play()
        {
            _despawnCooldown.Reset();
        }

        public virtual void Stop()
        {
            OnCompleted?.Invoke(this);

            this.StopAllCoroutines();
            OnCompleted = null;
        }
    }
}