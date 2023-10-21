using JetBrains.Annotations;

namespace Game.Character
{
    [UsedImplicitly]
    internal class CharacterStats
    {
        public int TotalHealth { get; }
        public int CurrentHealth { get; private set; }
        public float Damage { get; private set; }
        public bool IsInvulnerable { get; private set; }
        public float InvulnerableDuration { get; private set; }

        public CharacterStats(int health, float damage, float invulnerableDuration)
        {
            TotalHealth = health;
            CurrentHealth = health;
            Damage = damage;
            IsInvulnerable = true;
            InvulnerableDuration = invulnerableDuration;
        }

        public void UpdateDamage(float additionalDamage) => Damage += additionalDamage;

        public void UpdateHealth(int additionalHealth) => CurrentHealth += additionalHealth;

        public void SetInvulnerableState(bool isOn) => IsInvulnerable = isOn;
    }
}