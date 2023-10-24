using Game.Enemies.Data;
using UnityEngine;

namespace Game.Character.Data
{
    [CreateAssetMenu(fileName = "ExperienceAmountByEnemyTypeMap", menuName = "Game/Data", order = 1)]
    internal class ExperienceData : ScriptableObject
    {
        [System.Serializable]
        internal struct Element
        {
            public Type Type;
            public int Amount;
        }

        [field: SerializeField] public Element[] Elements { get; private set; }
    }
}