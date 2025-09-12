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

        public int AddMergeOrbScore(int level)
        {
            if (ScoreDefinition == null) return 0;
            int scoreToAdd = ScoreDefinition.GetOrbMergedScore(level);
            this.AddScore(scoreToAdd);
            return scoreToAdd;
        }

        public int AddDroppedScore()
        {
            if (ScoreDefinition == null) return 0;
            int scoreToAdd = ScoreDefinition.DropScore;
            this.AddScore(scoreToAdd);
            return scoreToAdd;
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }

        public void AddScoreToHistory()
        {
            HistoryScore newScore = new()
            {
                Score = CurrentScore,
                Time = System.DateTime.Now,
                Playtime = PlaytimeManager.Instance == null ? 0f : PlaytimeManager.Instance.Playtime,
                DroppedCount = Players.Player.Instance.Dropper.DropCount,
                MergedCount = Orbs.OrbManager.Instance.MergedCount,
            };

            HistoryScores.Add(newScore);
            if (BestScore < CurrentScore)
            {
                BestScore = CurrentScore;
            }
        }
    }
}
