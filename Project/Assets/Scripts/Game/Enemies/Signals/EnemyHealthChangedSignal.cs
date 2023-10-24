using Game.Enemies.Data;

namespace Game.Enemies.Signals
{
    internal class EnemyHealthChangedSignal : EnemySignal
    {
        public EnemyHealthChangedSignal(Model model) : base(model)
        {
        }
    }
}