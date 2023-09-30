using Game.Shared.Damage.Components;

namespace Game.Character.Components
{
    internal class DamageDealerComponent : IDamageDealer
    {
        public int Damage { get; private set; }

        public void SetDamage(int value) => Damage = value;
    }
}