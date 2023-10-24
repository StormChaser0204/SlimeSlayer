using Common.SignalHandler.Handlers;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using Game.UI.Health;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class EnemyHealthBarHandler : SignalHandler
    {
        [Inject] private HealthService _healthService;

        private readonly Model _model;

        public EnemyHealthBarHandler(EnemySignal signal) : base(signal) => _model = signal.Model;

        public override void Handle()
        {
            if (SignalIs<SpawnEnemySignal>())
                _healthService.InstantiateNewBar(_model);
            else if (SignalIs<EnemyDiedSignal>())
                _healthService.DisableHealthBar(_model);
            else
                _healthService.UpdateHealthBar(_model);
        }
    }
}