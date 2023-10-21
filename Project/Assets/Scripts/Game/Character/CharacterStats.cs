using JetBrains.Annotations;

namespace Game.Character
{
    [UsedImplicitly]
    internal class CharacterStats
    {
        public int TotalHealth { get; }
        public int CurrentHealth { get; private set; }
        public float Damage { get; private set; }

        public CharacterStats(int health, float damage)
        {
            TotalHealth = health;
            CurrentHealth = health;
            Damage = damage;
        }

        public void UpdateDamage(float additionalDamage) => Damage += additionalDamage;

        public void UpdateHealth(int additionalHealth) => CurrentHealth += additionalHealth;
    }
}