using System;
using UnityEngine;

namespace Asce.Game.Scores
{
    [Serializable]
    public class HistoryScore
    {
        [SerializeField] private DateTime _time = default;
        [SerializeField] private int _score = 0;
        [SerializeField] private float _playtime = 0f;
        [SerializeField] private int _droppedCount = 0;
        [SerializeField] private int _mergedCount = 0;

        public DateTime Time
        {
            get => _time;
            set => _time = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }
        public float Playtime
        {
            get => _playtime;
            set => _playtime = value;
        }
        public int DroppedCount
        {
            get => _droppedCount;
            set => _droppedCount = value;
        }
        public int MergedCount
        {
            get => _mergedCount;
            set => _mergedCount = value;
        }

        public HistoryScore() { }
        /// <summary>
        ///     Create history score from score and string datetime.
        ///     If the string cannot be parsed, fallback to default date.
        /// </summary>
        public HistoryScore(string dateTimeString)
        {
            this.SetTime(dateTimeString);
        }

        public void SetTime(DateTime time)
        {
            _time = time;
        }

        public void SetTime(string dateTimeString)
        {
            if (!DateTime.TryParse(dateTimeString, out _time))
            {
                _time = default;
            }
        }
    }
}
