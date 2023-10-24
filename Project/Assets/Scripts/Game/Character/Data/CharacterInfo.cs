using JetBrains.Annotations;
using UnityEngine;

namespace Game.Character.Data
{
    [UsedImplicitly]
    internal class CharacterInfo
    {
        public int TotalHealth { get; }
        public int CurrentHealth { get; private set; }

        public float Damage { get; private set; }
        public float AttackRange { get; private set; }
        public Vector3 AttackPosition { get; private set; }

        public bool IsInvulnerable { get; private set; }
        public float InvulnerableDuration { get; private set; }

        public CharacterInfo(int health, float damage, float attackRange, Vector3 attackPosition,
            float invulnerableDuration)
        {
            TotalHealth = health;
            CurrentHealth = health;
            Damage = damage;
            AttackRange = attackRange;
            AttackPosition = attackPosition;
            InvulnerableDuration = invulnerableDuration;
        }

        public void UpdateDamage(float additionalDamage) => Damage += additionalDamage;

        public void UpdateHealth(int additionalHealth) => CurrentHealth += additionalHealth;

        public void SetInvulnerableActiveState(bool isOn) => IsInvulnerable = isOn;
    }
}