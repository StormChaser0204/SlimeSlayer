using System;
using Game.Shared.Damage.Components;

namespace Game.Minions
{
    internal class DamageTaker : IDamageTaker
    {
        public int Health { get; set; }
        //{
        //    get => _health;
        //    set
        //    {
        //        _health = value;
        //        if (_health <= 0)
        //            OnDeath.Invoke(this);
        //    }
        //}
        public Action<IDamageTaker> OnDeath { get; set; }
    }
}