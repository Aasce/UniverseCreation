using System;
using UnityEngine;

namespace Asce.Game.Scores
{
    [Serializable]
    public class HistoryScore
    {
        [SerializeField] private DateTime _time = default;
        [SerializeField] private int _score = 0;

        public DateTime Time => _time;
        public int Score => _score;

        /// <summary>
        ///     Create history score from score and string datetime.
        ///     If the string cannot be parsed, fallback to default date.
        /// </summary>
        public HistoryScore(int score, string dateTimeString)
        {
            _score = score;
            if (!DateTime.TryParse(dateTimeString, out _time))
            {
                _time = default;
            }
        }

        /// <summary>
        ///     Create history score from score and DateTime.
        /// </summary>
        public HistoryScore(int score, DateTime dateTime)
        {
            _time = dateTime;
            _score = score;
        }
    }
}
