using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asce.Game.Orbs
{
    [CreateAssetMenu(menuName = "Asce/Orbs/Orbs Data", fileName = "Orbs Data")]
    public class SO_Orbs : ScriptableObject
    {
        [SerializeField] private List<Orb> _orbs = new();
        private ReadOnlyCollection<Orb> _readOnlyOrbs;
        private Dictionary<int, Orb> _orbDictionary;

        public ReadOnlyCollection<Orb> Orbs => _readOnlyOrbs ??= _orbs.AsReadOnly();
        
        public Orb GetOrbPrefab(int level)
        {
            if (_orbDictionary == null) this.InitDictionary();
            return _orbDictionary.TryGetValue(level, out Orb orb) ? orb : null;
        }

        private void InitDictionary()
        {
            _orbDictionary = new Dictionary<int, Orb>();
            foreach (Orb orb in _orbs)
            {
                if (orb.IsNull()) continue;
                if (orb.Information == null) continue;
                if (!_orbDictionary.ContainsKey(orb.Information.Level))
                    _orbDictionary.Add(orb.Information.Level, orb);
            }
        }
    }
}