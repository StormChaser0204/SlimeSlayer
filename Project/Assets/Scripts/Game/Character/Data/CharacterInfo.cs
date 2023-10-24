using JetBrains.Annotations;

namespace Game.Character.Data
{
    [UsedImplicitly]
    internal class CharacterInfo
    {
        public int TotalHealth { get; }
        public int CurrentHealth { get; private set; }
        public float Damage { get; private set; }
        public bool IsInvulnerable { get; private set; }
        public float InvulnerableDuration { get; private set; }
        public float AttackRange { get; private set; }

        public CharacterInfo(int health, float damage, float invulnerableDuration, float attackRange)
        {
            TotalHealth = health;
            CurrentHealth = health;
            Damage = damage;
            InvulnerableDuration = invulnerableDuration;
            AttackRange = attackRange;
        }

        public void UpdateDamage(float additionalDamage) => Damage += additionalDamage;

        public void UpdateHealth(int additionalHealth) => CurrentHealth += additionalHealth;

        public void SetInvulnerableActiveState(bool isOn) => IsInvulnerable = isOn;
    }
}