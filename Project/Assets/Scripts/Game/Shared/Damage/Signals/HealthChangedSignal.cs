using Game.Enemies.Data;
using Game.Shared.Damage.Components;

namespace Game.Shared.Damage.Signals
{
    internal class HealthChangedSignal
    {
        public readonly Model Model;

        public HealthChangedSignal(Model model) => Model = model;
    }
}