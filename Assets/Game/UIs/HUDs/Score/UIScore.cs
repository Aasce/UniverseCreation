using Asce.Game.Scores;
using Asce.Managers.UIs;
using Asce.Managers.Utils;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Asce.Game.UIs
{
    public class UIScore : UIObject
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private NumberFormatInfo _scoreFormat;

        private NumberFormatInfo ScoreFormat
        {
            get
            {
                if (_scoreFormat == null)
                {
                    _scoreFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                    _scoreFormat.NumberGroupSeparator = " "; // use space as thousand separator
                }
                return _scoreFormat;
            }
        }

        public void SetTitle(string title)
        {
            if (_titleText == null) return;
            _titleText.text = title;
        }

        public void SetScore(int score)
        {
            if (_scoreText == null) return;
            _scoreText.text = NumberUtils.AsThousandSeparator(score);
        }
    }
}
