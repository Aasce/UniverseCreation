using Asce.Game.Orbs;
using Asce.Game.Scores;
using Asce.Managers;
using Asce.Managers.SaveLoads;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.SaveLoads
{
    public class SaveLoadManager : MonoBehaviourSingleton<SaveLoadManager>
    {
        [SerializeField] private string _currentOrbsFile = "current/orbs.json";
        [SerializeField] private string _currentScoreFile = "current/score.json";
        [SerializeField] private string _currentDropperFile = "current/dropper.json";

        [Space]
        [SerializeField] private string _historyScoresFile = "history/scores.json";

        public void SaveCurrentGame()
        {
            SaveCurrentOrbs();
            SaveCurrentScore();
            SaveCurrentDropper();
        }

        public void LoadCurrentGame()
        {
            LoadCurrentOrbs();
            LoadCurrentScore();
            LoadCurrentDropper();
        }

        public void ClearCurrentGame()
        {
            ClearCurrentOrbs();
            ClearCurrentScore();
            ClearCurrentDropper();
        }

        public void SaveCurrentOrbs()
        {
            if (OrbManager.Instance == null) return;
            List<Orb> activeOrbs = OrbManager.Instance.GetAllActiveOrbs();
            OrbsDataCollection orbsData = new(activeOrbs);
            SaveLoadSystem.Save(orbsData, _currentOrbsFile);
        }

        public void LoadCurrentOrbs()
        {
            if (OrbManager.Instance == null) return;
            OrbsDataCollection orbsData = SaveLoadSystem.Load<OrbsDataCollection>(_currentOrbsFile);
            if (orbsData == null) return;

            foreach (OrbData orbData in orbsData.orbsData)
            {
                if (orbData == null) continue;
                if (orbData.level <= 0) continue;

                Orb orb = OrbManager.Instance.Spawn(orbData.level, orbData.position);
                if (orb.IsNull()) continue;
                orb.IsValid = orbData.isValid;
            }
        }

        public void ClearCurrentOrbs()
        {
            SaveLoadSystem.Clear(_currentOrbsFile);
        }

        public void SaveCurrentScore()
        {
            if (ScoreManager.Instance == null) return;
            CurrentScoreData current = new (ScoreManager.Instance.CurrentScore);
            SaveLoadSystem.Save(current, _currentScoreFile);
        }

        public void LoadCurrentScore()
        {
            if (ScoreManager.Instance == null) return;
            CurrentScoreData current = SaveLoadSystem.Load<CurrentScoreData>(_currentScoreFile);
            if (current == null) return;
            ScoreManager.Instance.CurrentScore = current.score;
        }

        public void ClearCurrentScore()
        {
            SaveLoadSystem.Clear(_currentScoreFile);
        }

        public void SaveCurrentDropper()
        {
            if (Players.Player.Instance == null) return;
            if (Players.Player.Instance.Dropper == null) return;

            DropperData dropperData = new (Players.Player.Instance.Dropper);
            SaveLoadSystem.Save(dropperData, _currentDropperFile);
        }

        public void LoadCurrentDropper()
        {
            if (Players.Player.Instance == null) return;
            if (Players.Player.Instance.Dropper == null) return;
            DropperData dropperData = SaveLoadSystem.Load<DropperData>(_currentDropperFile);
            if (dropperData == null) return;
            dropperData.Load(Players.Player.Instance.Dropper);
        }

        public void ClearCurrentDropper()
        {
            SaveLoadSystem.Clear(_currentDropperFile);
        }

        #region History Scores
        public void LoadHistoryScore()
        {
            ScoreHistoryData historyData = SaveLoadSystem.Load<ScoreHistoryData>(_historyScoresFile);
            historyData ??= new ScoreHistoryData();

            ScoreManager.Instance.BestScore = historyData.bestScore.score;
            foreach (ScoreData scoreData in historyData.scores)
            {
                if (scoreData == null) continue;
                HistoryScore historyScore = new(scoreData.score, scoreData.time);
                ScoreManager.Instance.HistoryScores.Add(historyScore);
            }
        }

        public void SaveHistoryScores()
        {
            ScoreHistoryData historyData = new ();
            foreach (HistoryScore historyScore in ScoreManager.Instance.HistoryScores)
            {
                if (historyScore == null) continue;
                historyData.AddScore(historyScore.Score, historyScore.Time);
            }
            SaveLoadSystem.Save(historyData, _historyScoresFile);
        }

        public void ClearHistoryScores()
        {
            SaveLoadSystem.Clear(_historyScoresFile);
        }
        #endregion
    }
}
