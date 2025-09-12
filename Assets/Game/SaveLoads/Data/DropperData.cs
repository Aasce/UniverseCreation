using Asce.Game.Orbs;
using Asce.Game.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.SaveLoads
{
    [System.Serializable]
    public class DropperData
    {
        public int currentOrbLevel = 0;
        public List<int> nextOrbLevels;
        public float cooldown = 0f;
        public int droppedCount = 0;

        public DropperData(Dropper dropper) 
        {
            if (dropper == null) return;
            currentOrbLevel = dropper.CurrentOrb.IsNull() ? 0 : dropper.CurrentOrb.Information.Level;
            nextOrbLevels = new(dropper.NextQueue);
            cooldown = dropper.DropCooldown.CurrentTime;
            droppedCount = dropper.DropCount;
        }

        public void Load(Dropper dropper)
        {
            if (dropper == null) return;
            if (currentOrbLevel > 0)
            {
                Orb orb = OrbManager.Instance.Spawn(currentOrbLevel, dropper.transform.position);
                if (!orb.IsNull())
                {
                    orb.Rigidbody.simulated = false;
                    orb.IsMerged = true;
                }

                dropper.CurrentOrb = orb;
            }
            else dropper.CurrentOrb = null;

            dropper.NextQueue.Clear();
            foreach (int level in nextOrbLevels)
            {
                dropper.NextQueue.Enqueue(level);
            }

            dropper.DropCooldown.CurrentTime = cooldown;
            dropper.DropCount = droppedCount;
        }
    }
}