using System.Globalization;
using UnityEngine;

namespace Asce.Managers.Utils
{
    public static class NumberUtils
    {
        private static NumberFormatInfo _scoreFormat;

        public static NumberFormatInfo ScoreFormat
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


        public static int GetIntegerDigitCount(float number)
        {
            number = Mathf.Abs(number);
            int intPart = (int)Mathf.Floor(number);

            if (intPart == 0)
                return 1;

            return (int)Mathf.Floor(Mathf.Log10(intPart)) + 1;
        }

        public static string AsThousandSeparator(int number)
        {
            return number.ToString("N0", ScoreFormat);
        }

        public static string FloatToTime(float value)
        {
            int minutes = Mathf.FloorToInt(value / 60f);
            int seconds = Mathf.FloorToInt(value % 60f);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}