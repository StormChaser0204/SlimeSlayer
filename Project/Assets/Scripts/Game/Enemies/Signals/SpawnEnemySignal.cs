using Game.Enemies.Data;

namespace Game.Enemies.Signals
{
    internal class SpawnEnemySignal
    {
        public readonly Model Model;

        public SpawnEnemySignal(Model model) => Model = model;
    }
}