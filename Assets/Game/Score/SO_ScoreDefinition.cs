using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asce.Game.Scores
{
    [CreateAssetMenu(menuName = "Asce/Score/Score Definition", fileName = "Score Definition")]
    public class SO_ScoreDefinition : ScriptableObject
    {
        [SerializeField] private int _droppedScore = 0;

        [Space]
        [SerializeField] private List<OrbMergedScore> _orbMergedScore = new();
        private ReadOnlyCollection<OrbMergedScore> _readOnlyOrbMergedScore;
        private Dictionary<int, int> _orbMergedScoreDictionary;

        public int DropScore => _droppedScore;
        public ReadOnlyCollection<OrbMergedScore> OrbMergedScore => _readOnlyOrbMergedScore ??= _orbMergedScore.AsReadOnly();

        public int GetOrbMergedScore(int orbLevel)
        {
            if (_orbMergedScoreDictionary == null) InitDictionary();
            return _orbMergedScoreDictionary.TryGetValue(orbLevel, out var score) ? score : 0;
        }

        private void InitDictionary()
        {
            _orbMergedScoreDictionary = new Dictionary<int, int>();
            foreach (OrbMergedScore item in _orbMergedScore)
            {
                if (_orbMergedScoreDictionary.ContainsKey(item.OrbLevel)) continue;
                _orbMergedScoreDictionary.Add(item.OrbLevel, item.Score);
            }
        }
    }


    [System.Serializable]
    public class OrbMergedScore
    {
        [SerializeField, Min(0)] private int _orbLevel = 0;
        [SerializeField, Min(0)] private int _score = 0;

        public int OrbLevel => _orbLevel;
        public int Score => _score;
    }
}
