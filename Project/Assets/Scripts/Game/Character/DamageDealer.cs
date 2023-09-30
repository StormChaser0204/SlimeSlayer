using Game.Shared.Damage.Components;

namespace Game.Character
{
    public class DamageDealer : IDamageDealer
    {
        public int Damage { get; private set; }

        public DamageDealer(int damage) => Damage = damage;
    }
}