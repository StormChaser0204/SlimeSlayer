using Game.Shared.Damage.Components;

namespace Game.Minions
{
    internal class DamageTaker : IDamageTaker
    {
        public int MaxHealth { get; set; }
        public int Health { get; set; }

        public DamageTaker(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }
    }
}