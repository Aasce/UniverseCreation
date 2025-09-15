using UnityEngine;

namespace Asce.Shared.Audios
{
    [CreateAssetMenu(menuName = "Asce/Audios/Audio Information", fileName = "Audio Information")]
    public class SO_AudioInformation : ScriptableObject
    {
        [Header("Basic Info")]
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;

        [Header("Audio Source Settings (2D Only)")]
        [Range(0f, 1f)]
        [SerializeField] private float _volume = 1f;
        [Range(-3f, 3f)]
        [SerializeField] private float _pitch = 1f;
        [SerializeField] private bool _loop = false;
        [SerializeField] private bool _playOnAwake = false;

        public string Name => _name;
        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public bool Loop => _loop;
        public bool PlayOnAwake => _playOnAwake;
    }
}
