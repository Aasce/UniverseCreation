using System;
using System.Collections.Generic;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class ScoreData
    {
        public string time;   // store as string ISO 8601 for JSON compatibility
        public int score;

        public ScoreData(int score) : this(score, DateTime.Now) { }
        public ScoreData(int score, DateTime time) 
        {
            this.time = time.ToString("o");
            this.score = score;
        }
    }
}
