using Asce.Game.Scores;
using System;
using System.Collections.Generic;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class ScoreData
    {
        public string time;   // store as string ISO 8601 for JSON compatibility
        public int score;
        public float playtime = 0f; // in seconds
        public int droppedCount = 0;
        public int mergedCount = 0;

        public ScoreData(HistoryScore historyScore) 
        {
            if (historyScore == null) return;

            this.time = historyScore.Time.ToString("o");
            this.score = historyScore.Score;
            this.playtime = historyScore.Playtime;
            this.droppedCount = historyScore.DroppedCount;
            this.mergedCount = historyScore.MergedCount;
        }

        public HistoryScore Create()
        {
            return new(time)
            {
                Score = score,
                Playtime = playtime,
                DroppedCount = droppedCount,
                MergedCount = mergedCount
            };
        }
    }
}
