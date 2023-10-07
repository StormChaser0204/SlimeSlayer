using Game.Minions.Data;

namespace Game.Minions.Signals
{
    internal class SpawnMinionSignal
    {
        public readonly Model Model;

        public SpawnMinionSignal(Model model) => Model = model;
    }
}