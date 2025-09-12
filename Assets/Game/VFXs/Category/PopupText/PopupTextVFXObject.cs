using TMPro;
using UnityEngine;

namespace Asce.Game.VFXs
{
    public class PopupTextVFXObject : VFXObject
    {
        [SerializeField] private TextMeshPro _text;

        public TextMeshPro Text => _text;

        public void SetText(string text)
        {
            if (_text == null) return;
            _text.text = text;
        }
    }
}
