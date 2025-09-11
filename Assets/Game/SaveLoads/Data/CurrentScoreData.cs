using System;
using System.Collections.Generic;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class CurrentScoreData
    {
        public int score;

        public CurrentScoreData(int score)
        {
            this.score = score;
        }
    }
}
