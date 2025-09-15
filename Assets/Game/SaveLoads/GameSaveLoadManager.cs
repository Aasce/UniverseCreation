using Asce.Game.Orbs;
using Asce.Game.Scores;
using Asce.Managers;
using Asce.Managers.SaveLoads;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.SaveLoads
{
    public class GameSaveLoadManager : MonoBehaviourSingleton<GameSaveLoadManager>
    {
        [SerializeField] private string _currentOrbsFile = "game/current_orbs.json";
        [SerializeField] private string _currentDropperFile = "game/current_dropper.json";
        [SerializeField] private string _currentScoreFile = "game/current_score.json";
        [SerializeField] private string _currentPlaytimeFile = "game/current_playtime.json";

        [Space]
        [SerializeField] private string _historyScoresFile = "game/history_scores.json";

        public void SaveCurrentGame()
        {
            SaveCurrentOrbs();
            SaveCurrentDropper();
            SaveCurrentScore();
            SaveCurrentPlaytime();
        }

        public void LoadCurrentGame()
        {
            LoadCurrentOrbs();
            LoadCurrentDropper();
            LoadCurrentScore();
            LoadCurrentPlaytime();
        }

        public void DeleteCurrentGame()
        {
            DeleteCurrentOrbs();
            DeleteCurrentDropper();
            DeleteCurrentScore();
            DeleteCurrentPlaytime();
        }

        public void SaveCurrentOrbs()
        {
            if (OrbManager.Instance == null) return;
            List<Orb> activeOrbs = OrbManager.Instance.GetAllActiveOrbs();
            OrbsDataCollection orbsData = new(activeOrbs);
            orbsData.mergerCount = OrbManager.Instance.MergedCount;
            SaveLoadSystem.Save(orbsData, _currentOrbsFile);
        }
        public void SaveCurrentDropper()
        {
            if (Players.Player.Instance == null) return;
            if (Players.Player.Instance.Dropper == null) return;

            DropperData dropperData = new (Players.Player.Instance.Dropper);
            SaveLoadSystem.Save(dropperData, _currentDropperFile);
        }
        public void SaveCurrentScore()
        {
            if (ScoreManager.Instance == null) return;
            CurrentScoreData current = new (ScoreManager.Instance.CurrentScore);
            SaveLoadSystem.Save(current, _currentScoreFile);
        }
        public void SaveCurrentPlaytime()
        {
            if (PlaytimeManager.Instance == null) return;
            PlaytimeData playtimeData = new(PlaytimeManager.Instance.Playtime);
            SaveLoadSystem.Save(playtimeData, _currentPlaytimeFile);
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
            OrbManager.Instance.MergedCount = orbsData.mergerCount;
        }
        public void LoadCurrentDropper()
        {
            if (Players.Player.Instance == null) return;
            if (Players.Player.Instance.Dropper == null) return;
            DropperData dropperData = SaveLoadSystem.Load<DropperData>(_currentDropperFile);

            if (dropperData == null) Players.Player.Instance.Dropper.ResetDropper();
            else dropperData.Load(Players.Player.Instance.Dropper);
        }
        public void LoadCurrentScore()
        {
            if (ScoreManager.Instance == null) return;
            CurrentScoreData current = SaveLoadSystem.Load<CurrentScoreData>(_currentScoreFile);
            if (current == null) ScoreManager.Instance.CurrentScore = 0;
            else ScoreManager.Instance.CurrentScore = current.score;
        }
        public void LoadCurrentPlaytime()
        {
            if (PlaytimeManager.Instance == null) return;
            PlaytimeData playtimeData = SaveLoadSystem.Load<PlaytimeData>(_currentPlaytimeFile);
            if (playtimeData == null) PlaytimeManager.Instance.StartTimer();
            else
            {
                PlaytimeManager.Instance.Playtime = playtimeData.playtime;
                PlaytimeManager.Instance.ResumeTimer();
            }
        }


        public void DeleteCurrentOrbs() => SaveLoadSystem.Delete(_currentOrbsFile);
        public void DeleteCurrentDropper() => SaveLoadSystem.Delete(_currentDropperFile);
        public void DeleteCurrentScore() => SaveLoadSystem.Delete(_currentScoreFile);
        public void DeleteCurrentPlaytime() => SaveLoadSystem.Delete(_currentPlaytimeFile);


        #region History Scores
        public void LoadHistoryScore()
        {
            ScoreHistoryData historyData = SaveLoadSystem.Load<ScoreHistoryData>(_historyScoresFile);
            historyData ??= new ScoreHistoryData();

            ScoreManager.Instance.BestScore = historyData.BestScore;
            foreach (ScoreData scoreData in historyData.scores)
            {
                if (scoreData == null) continue;
                ScoreManager.Instance.HistoryScores.Add(scoreData.Create());
            }
        }

        public void SaveHistoryScores()
        {
            ScoreHistoryData historyData = new ();
            foreach (HistoryScore historyScore in ScoreManager.Instance.HistoryScores)
            {
                if (historyScore == null) continue;
                ScoreData scoreData = new(historyScore);
                historyData.AddScore(scoreData);
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
