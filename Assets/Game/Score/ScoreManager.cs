using Asce.Managers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asce.Game.Scores
{
    public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
    {
        [SerializeField] private SO_ScoreDefinition _scoreDefinition;
        [SerializeField] private int _currentScore = 0;
        [SerializeField] private int _bestScore = 0;
        [SerializeField] private List<HistoryScore> _historyScores = new();

        public event System.Action<object, int> OnScoreChanged;
        public event System.Action<object, int> OnBestScoreChanged;


        public SO_ScoreDefinition ScoreDefinition => _scoreDefinition;
        public List<HistoryScore> HistoryScores => _historyScores;

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                if (_currentScore != value)
                {
                    _currentScore = value;
                    OnScoreChanged?.Invoke(this, _currentScore);
                }
            }
        }
        public int BestScore
        {
            get => _bestScore;
            set
            {
                if (_bestScore != value)
                {
                    _bestScore = value;
                    OnBestScoreChanged?.Invoke(this, _bestScore);
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

        public void AddScoreToHistory()
        {
            HistoryScore newScore = new(CurrentScore, System.DateTime.Now);
            HistoryScores.Add(newScore);
            if (BestScore < CurrentScore)
            {
                BestScore = CurrentScore;
            }
        }
    }
}
