using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Game.Enemies.Data;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.Enemies.Handlers
{
    [UsedImplicitly]
    internal class ActiveEnemiesHandler : SignalHandler
    {
        [Inject] private SpawnService _spawnService;
        [Inject] private ActiveEnemies _activeEnemies;

        private readonly Model _model;

        public ActiveEnemiesHandler(EnemySignal signal) : base(signal) => _model = signal.Model;

        public override void Handle()
        {
            if (SignalIs<EnemyDiedSignal>())
            {
                _activeEnemies.Remove(_model);
                _spawnService.Deactivate(SignalAs<EnemyDiedSignal>().Model);
            }
            else
            {
                _activeEnemies.Add(_model);
            }
        }
    }
}