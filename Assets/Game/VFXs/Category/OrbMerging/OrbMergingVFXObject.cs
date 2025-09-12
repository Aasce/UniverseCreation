using UnityEngine;

namespace Asce.Game.VFXs
{
    public class OrbMergingVFXObject : VFXObject
    {
        [SerializeField] private ParticleSystem _sparkParticles;
        [SerializeField] private ParticleSystem _flashParticles;

        public ParticleSystem SparkParticles => _sparkParticles;
        public ParticleSystem FlashParticles => _flashParticles;
    }
}
