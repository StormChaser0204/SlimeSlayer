using Game.Enemies.Data;

namespace Game.Character.Signals
{
    internal class EnemyDiedSignal
    {
        public readonly Model Model;

        public EnemyDiedSignal(Model model) => Model = model;
    }
}