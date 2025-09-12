using System;
using System.Collections.Generic;

namespace Asce.Game.SaveLoads
{
    [Serializable]
    public class ScoreHistoryData
    {
        public ScoreData bestScore;
        public List<ScoreData> scores = new ();

        public int BestScore => bestScore == null ? 0 : bestScore.score;

        public void AddScore(ScoreData newScore)
        {
            if (newScore == null) return;
            scores.Add(newScore);
            if (bestScore == null || newScore.score > bestScore.score)
            {
                bestScore = newScore;
            }
        }
    }
}
