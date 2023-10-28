using Common.SignalHandler.Handlers;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using Game.UI.Health;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class EnemyHealthBarInstanceHandler : SignalHandler
    {
        [Inject] private HealthService _healthService;

        private readonly Model _model;

        public EnemyHealthBarInstanceHandler(EnemySignal signal) : base(signal) =>
            _model = signal.Model;

        public override void Handle()
        {
            if (SignalIs<SpawnEnemySignal>())
                _healthService.InstantiateNewBar(_model);
            else
                _healthService.DisableHealthBar(_model);
        }
    }
}