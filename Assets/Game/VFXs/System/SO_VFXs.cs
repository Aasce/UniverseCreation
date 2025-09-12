using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asce.Game.VFXs
{
    [CreateAssetMenu(menuName = "Asce/VFXs/VFXs Data", fileName = "VFXs Data")]
    public class SO_VFXs : ScriptableObject
    {
        [SerializeField] private List<VFXInformation> _vfxs = new();
        private ReadOnlyCollection<VFXInformation> _vfxsReadOnly;
        private Dictionary<string, VFXInformation> _vfxsDictionary;


        public ReadOnlyCollection<VFXInformation> VFXs => _vfxsReadOnly ??= _vfxs.AsReadOnly();

        public VFXInformation GetVFX(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            if (_vfxsDictionary == null) this.InitDictionary();
            if (_vfxsDictionary.TryGetValue(name, out VFXInformation vfxInfo))
            {
                return vfxInfo;
            }

            Debug.LogWarning($"VFX with name {name} not found in the dictionary.");
            return null;
        }

        private void InitDictionary()
        {
            _vfxsDictionary = new Dictionary<string, VFXInformation>(StringComparer.OrdinalIgnoreCase);
            foreach (VFXInformation vfx in _vfxs)
            {
                if (vfx == null) continue;
                if (!_vfxsDictionary.ContainsKey(vfx.Name))
                {
                    _vfxsDictionary.Add(vfx.Name, vfx);
                }
                else Debug.LogWarning($"VFX with name {vfx.Name} already exists in the dictionary. Skipping duplicate.");
            }
        }
    }
}
