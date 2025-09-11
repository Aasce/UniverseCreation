using System;
using Asce.Game.Orbs;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class OrbsDataCollection
    {
        public List<OrbData> orbsData = new ();

        public OrbsDataCollection(List<Orb> orbs)
        {
            if (orbs == null) return;
            foreach (Orb orb in orbs)
            {
                if (orb.IsNull()) continue;
                if (orb.IsHoldByDropper()) continue;

                OrbData orbData = new(orb);
                orbsData.Add(orbData);
            }
        }
    }
}