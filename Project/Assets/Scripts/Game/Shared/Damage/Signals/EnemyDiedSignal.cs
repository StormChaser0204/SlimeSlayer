using Game.Enemies.Data;

namespace Game.Shared.Damage.Signals
{
    internal class EnemyDiedSignal
    {
        public readonly Model Model;

        public EnemyDiedSignal(Model model) => Model = model;
    }
}