using UnityEngine;

namespace Game.Character.Data
{
    [CreateAssetMenu(fileName = "BaseStatsData", menuName = "Game/Data/BaseStatsData", order = 1)]
    internal class BaseStatsData : ScriptableObject
    {
        public int Health;
        public float Damage;
        public float AttackRange;
        public float AttackCooldown;
        public float BlockCooldown;
        public float InvulnerabilityDuration;
        public float MovementSpeed;
    }
}