using UnityEngine;

namespace Asce.Game.UIs
{

    [System.Serializable]
    public class RecordRankContainer
    {
        [SerializeField] private int _rank;
        [SerializeField, ColorUsage(showAlpha: true)] private Color _color;

        public int Rank => _rank;
        public Color Color => _color;
    }
}