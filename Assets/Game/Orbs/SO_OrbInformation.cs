using Asce.Managers.Attributes;
using UnityEngine;

namespace Asce.Game.Orbs
{
    [CreateAssetMenu(menuName = "Asce/Orbs/Orb Information", fileName = "Orb Information")]
    public class SO_OrbInformation : ScriptableObject
    {
        [SerializeField, Min(0)] private int _level = 0;
        [SerializeField, SpritePreview] private Sprite _icon;

        public int Level => _level;
        public Sprite Icon => _icon;
    }
}