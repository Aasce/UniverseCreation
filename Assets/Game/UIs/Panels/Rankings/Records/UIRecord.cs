using Asce.Game.Scores;
using Asce.Managers.UIs;
using Asce.Managers.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Game.UIs
{
    public class UIRecord : UIObject
    {
        [SerializeField] private Image _rankBackground;
        [SerializeField] private TextMeshProUGUI _rank;

        [Space]
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _date;

        [Space]
        [SerializeField] private TextMeshProUGUI _playtime;
        [SerializeField] private TextMeshProUGUI _drop;
        [SerializeField] private TextMeshProUGUI _merge;

        public void Set(HistoryScore score)
        {
            if (score == null) return;
            if (_score != null) _score.text = NumberUtils.AsThousandSeparator(score.Score);
            if (_date != null) _date.text = score.Time.ToString();

            if (_playtime != null) _playtime.text = NumberUtils.FloatToTime(score.Playtime);
            if (_drop != null) _drop.text = NumberUtils.AsThousandSeparator(score.DroppedCount);
            if (_merge != null) _merge.text = NumberUtils.AsThousandSeparator(score.MergedCount);
        }

        public void SetRank(int rank, Color color)
        {
            if (_rank != null)
            {
                _rank.text = rank.ToString();
                _rank.color = color;
            }

            if (_rankBackground != null)
            {
                _rankBackground.color = color;
            }
        }
    }
}
