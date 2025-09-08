using UnityEngine;

namespace Asce.Game.Orbs
{
    public static class OrbExtension
    {
        public static bool IsNull(this Orb orb) => orb == null || orb.Information == null;

        public static bool CanMerge(this Orb orbA, Orb orbB)
        {
            if (orbA.IsNull() || orbB.IsNull()) return false;
            return orbA.Information.Level == orbB.Information.Level;
        }
    }
}