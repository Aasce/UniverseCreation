using System;
using UnityEngine;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class PlaytimeData
    {
        public float playtime = 0f;

        public PlaytimeData(float playtime)
        {
            this.playtime = playtime;
        }
    }

}