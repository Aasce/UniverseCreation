using Asce.Game.Orbs;
using System;
using UnityEngine;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class OrbData
    {
        public int level = 0;
        public Vector2 position = Vector2.zero;
        public bool isValid = true;

        public OrbData(Orb orb)
        {
            if (orb.IsNull()) return;
            level = orb.Information.Level;
            position = orb.transform.position;
            isValid = orb.IsValid;
        }
    }
}