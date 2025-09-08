using UnityEngine;

namespace Asce.Game.Orbs
{
    [CreateAssetMenu(menuName = "Asce/Orbs/Orb Information", fileName = "Orb Information")]
    public class SO_OrbInformation : ScriptableObject
    {
        [SerializeField, Min(0)] private int _level = 0;


        public int Level => _level;
    }
}