using System;
using Game.Enemies.Data;

namespace Game.Shared.Damage
{
    internal class DamageProcessor
    {
        public Action<Model> OnHealthChanged { get; set; }
        public Action<Model> OnDeath { get; set; }

        public void Process(float damage, Model model)
        {
            model.Health -= damage;
            OnHealthChanged.Invoke(model);
            if (model.Health > 0)
                return;

            OnDeath.Invoke(model);
        }
    }
}