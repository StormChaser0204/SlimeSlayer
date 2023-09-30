using System;

namespace Game.Shared.Damage.Components
{
    //rename to health
    public interface IDamageTaker
    {
        public int Health { get; set; }
        public Action<IDamageTaker> OnDeath { get; set; }
    }
}