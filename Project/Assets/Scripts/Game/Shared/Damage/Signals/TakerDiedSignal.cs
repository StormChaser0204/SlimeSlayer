using Game.Shared.Damage.Components;

namespace Game.Shared.Damage.Signals
{
    internal class TakerDiedSignal
    {
        public readonly IDamageTaker Taker;

        public TakerDiedSignal(IDamageTaker taker) => Taker = taker;
    }
}