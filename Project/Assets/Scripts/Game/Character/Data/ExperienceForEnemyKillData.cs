using Game.Enemies.Data;
using UnityEngine;

namespace Game.Character.Data
{
    [CreateAssetMenu(fileName = "ExperienceForEnemyKillData", menuName = "Game/Data/ExperienceForEnemyKillData", order = 1)]
    internal class ExperienceForEnemyKillData : ScriptableObject
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