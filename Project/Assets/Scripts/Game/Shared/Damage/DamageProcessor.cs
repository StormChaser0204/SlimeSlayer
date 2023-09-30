using System;
using Game.Shared.Damage.Components;

namespace Game.Shared.Damage
{
    internal class DamageProcessor
    {
        public Action OnKill { get; set; }

        public void Process(IDamageDealer dealer, IDamageTaker taker)
        {
            taker.Health -= dealer.Damage;
            if (taker.Health > 0)
                return;

            taker.OnDeath.Invoke(taker);
            OnKill.Invoke();
        }
    }
}