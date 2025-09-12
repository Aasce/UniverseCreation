using UnityEngine;

namespace Asce.Game.VFXs
{
    [System.Serializable]
    public class VFXInformation
    {
        [SerializeField] private string _name = "VFX Name";
        [SerializeField] private VFXObject _vfxPrefab;
        [SerializeField, Min(0f)] private float _duration = 1f;

        public string Name => _name;
        public VFXObject VFXPrefab => _vfxPrefab;
        public float Duration => _duration;
    }
}