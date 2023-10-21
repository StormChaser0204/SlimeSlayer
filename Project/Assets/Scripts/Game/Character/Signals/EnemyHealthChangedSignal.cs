using Game.Enemies.Data;

namespace Game.Character.Signals
{
    internal class EnemyHealthChangedSignal
    {
        public readonly Model Model;

        public EnemyHealthChangedSignal(Model model) => Model = model;
    }
}