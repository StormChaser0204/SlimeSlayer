using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.Enemies.Handlers
{
    [UsedImplicitly]
    internal class PoolHandler : SignalHandler
    {
        [Inject] private SpawnService _spawnService;

        public PoolHandler(ISignal signal) : base(signal)
        {
        }

        public override void Handle()
        {
            if (SignalIs<EnemyDiedSignal>())
            {
                SpawnService.ReturnToPool(SignalAs<EnemyDiedSignal>().Model);
            }
        }
    }
}