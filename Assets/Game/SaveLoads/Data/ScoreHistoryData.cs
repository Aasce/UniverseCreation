using System;
using System.Collections.Generic;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class ScoreHistoryData
    {
        public ScoreData bestScore;
        public List<ScoreData> scores = new ();

        public void AddScore(int score, DateTime time)
        {
            ScoreData newScore = new (score, time);
            scores.Add(newScore);

            if (bestScore == null || score > bestScore.score)
            {
                bestScore = newScore;
            }
        }
    }
}
