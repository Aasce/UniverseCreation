using UnityEngine;

namespace Asce.Game.Orbs
{
    public static class OrbExtension
    {
        public static bool IsNull(this Orb orb) => orb == null || orb.Information == null;

        public static bool CanMerge(this Orb orbA, Orb orbB)
        {
            if (orbA.IsNull() || orbB.IsNull()) return false;
            if (orbA.IsMerged || orbB.IsMerged) return false;
            return orbA.Information.Level == orbB.Information.Level;
        }

        public static bool IsHoldByDropper(this Orb orb)
        {
            if (orb.IsNull()) return false;
            if (Players.Player.Instance == null) return false;
            if (Players.Player.Instance.Dropper == null) return false;

            Orb holdedOrb = Players.Player.Instance.Dropper.CurrentOrb;
            if (holdedOrb == null) return false;

            return orb == holdedOrb;
        }
    }
}