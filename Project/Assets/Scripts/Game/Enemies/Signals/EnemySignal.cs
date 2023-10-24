using Common.SignalHandler;
using Game.Enemies.Data;

namespace Game.Enemies.Signals
{
    internal class EnemySignal : ISignal
    {
        public readonly Model Model;

        protected EnemySignal(Model model) => Model = model;
    }
}