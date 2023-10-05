using System;
using Game.Shared.Damage.Components;

namespace Game.Shared.Damage
{
    internal class DamageProcessor
    {
        public Action<IDamageTaker> OnHealthChanged { get; set; }
        public Action<IDamageTaker> OnDeath { get; set; }

        public void Process(IDamageDealer dealer, IDamageTaker taker)
        {
            taker.Health -= dealer.Damage;
            OnHealthChanged.Invoke(taker);
            if (taker.Health > 0)
                return;

            OnDeath.Invoke(taker);
        }
    }
}