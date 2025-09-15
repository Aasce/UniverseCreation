using Asce.Game.Scores;
using Asce.Managers.Pools;
using Asce.Shared.UIs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIRankings : UIPanel
    {
        [Header("Action Buttons")]
        [SerializeField] private Button _backButton;

        [Space]
        [SerializeField] private Pool<UIRecord> _recordPool;

        [Header("Settings")]
        [SerializeField] private int _recordCount = 10;
        [SerializeField] private List<RecordRankContainer> _recordRanks = new();
        private Dictionary<int, RecordRankContainer> _recordRanksDictionary;

        private void Start()
        {
            if (_backButton != null) _backButton.onClick.AddListener(this.Hide);
        }

        public override void Show()
        {
            if (IsShow) return;

            if (_recordRanksDictionary == null) this.InitDictionary();
            _recordPool.Clear();

            List<HistoryScore> historyScores = ScoreManager.Instance.HistoryScores.OrderByDescending(s => s.Score).ToList();
            int maxCount = Mathf.Min(historyScores.Count, _recordCount);
            for (int i = 0; i < maxCount; i++)
            {
                HistoryScore score = historyScores[i];
                if (score ==  null) continue;

                UIRecord record = _recordPool.Activate();
                if (record == null) continue;

                Color color = _recordRanksDictionary.TryGetValue(i + 1, out RecordRankContainer container) 
                    ? container.Color 
                    : Color.white;

                record.SetRank(i + 1, color);
                record.Set(score);
                record.transform.SetAsLastSibling();
            }

            base.Show();
        }

        private void InitDictionary()
        {
            _recordRanksDictionary = new Dictionary<int, RecordRankContainer>();
            foreach (RecordRankContainer container in _recordRanks)
            {
                if (container == null) continue;
                if (_recordRanksDictionary.ContainsKey(container.Rank)) continue;
                _recordRanksDictionary[container.Rank] = container;
            }
        }
    }
}
