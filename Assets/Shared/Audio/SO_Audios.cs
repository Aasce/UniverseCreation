using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asce.Shared.Audios
{
    [CreateAssetMenu(menuName = "Asce/Audios/Audios Data", fileName = "Audios Data")]
    public class SO_Audios : ScriptableObject
    {
        [SerializeField] private List<SO_AudioInformation> _audios = new();
        private ReadOnlyCollection<SO_AudioInformation> _readonlyAudios;
        private Dictionary<string, SO_AudioInformation> _audiosDictionary;

        public ReadOnlyCollection<SO_AudioInformation> Audios => _readonlyAudios ??= _audios.AsReadOnly();

        public SO_AudioInformation Get(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            if (_audiosDictionary == null) this.InitDictionary();
            if (_audiosDictionary.TryGetValue(name, out SO_AudioInformation audio))
            {
                if (audio.IsValid()) return audio;
            }
            return null;
        }

        private void InitDictionary()
        {
            _audiosDictionary = new Dictionary<string, SO_AudioInformation>(StringComparer.OrdinalIgnoreCase);
            foreach (SO_AudioInformation audio in _audios)
            {
                if (!audio.IsValid(isNameValid: true)) continue;
                if (_audiosDictionary.ContainsKey(audio.Name)) continue;
                _audiosDictionary[audio.Name] = audio;
            }
        }
    }
}