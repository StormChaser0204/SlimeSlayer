using Game.Enemies.Data;

namespace Game.Shared.Damage.Signals
{
    internal class HealthChangedSignal
    {
        public readonly Model Model;

        public HealthChangedSignal(Model model) => Model = model;
    }
}