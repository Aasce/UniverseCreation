using UnityEngine;

namespace Asce.Shared.Audios
{
    public static class AudioExtension
    {
        public static bool IsValid(this SO_AudioInformation audio, bool isNameValid = false)
        {
            if (audio == null) return false;
            if (audio.Clip == null) return false;
            if (isNameValid) 
                if (string.IsNullOrEmpty(audio.Name)) 
                    return false;

            return true;
        }
        public static void Set(AudioSource source, SO_AudioInformation info)
        {
            if (source == null) return;
            if (!info.IsValid()) return;

            source.name = info.Name;
            source.clip = info.Clip;
            source.volume = info.Volume;
            source.pitch = info.Pitch;
            source.loop = info.Loop;
            source.playOnAwake = info.PlayOnAwake;

            // Force 2D
            source.spatialBlend = 0f;
        }
    }
}