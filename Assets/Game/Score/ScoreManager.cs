using Asce.Managers;
using UnityEngine;

namespace Asce.Game.Scores
{
    public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
    {
        [SerializeField] private SO_ScoreDefinition _scoreDefinition;
        [SerializeField] private int _currentScore = 0;

        public event System.Action<object, int> OnScoreChanged;


        public SO_ScoreDefinition ScoreDefinition => _scoreDefinition;

        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                if (_currentScore != value)
                {
                    _currentScore = value;
                    OnScoreChanged?.Invoke(this, _currentScore);
                }
            }
        }

        public void AddScore(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;
        }

        public void AddMergeOrbScore(int level)
        {
            if (ScoreDefinition == null) return;
            int scoreToAdd = ScoreDefinition.GetOrbMergedScore(level);
            this.AddScore(scoreToAdd);
        }

        public void AddDroppedScore()
        {
            if (ScoreDefinition == null) return;
            int scoreToAdd = ScoreDefinition.DropScore;
            this.AddScore(scoreToAdd);
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }
    }
}
