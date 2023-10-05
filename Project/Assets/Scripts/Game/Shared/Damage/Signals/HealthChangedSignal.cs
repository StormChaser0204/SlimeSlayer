using Game.Shared.Damage.Components;

namespace Game.Shared.Damage.Signals
{
    internal class HealthChangedSignal
    {
        public readonly IDamageTaker Taker;

        public HealthChangedSignal(IDamageTaker taker) => Taker = taker;
    }
}