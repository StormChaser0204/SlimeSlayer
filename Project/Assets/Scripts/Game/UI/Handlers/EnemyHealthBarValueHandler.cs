using Common.SignalHandler.Handlers;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using Game.UI.Health;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class EnemyHealthBarValueHandler : SignalHandler
    {
        [Inject] private HealthService _healthService;

        private readonly Model _model;

        public EnemyHealthBarValueHandler(EnemyHealthChangedSignal signal) : base(signal) =>
            _model = signal.Model;

        public override void Handle() => _healthService.UpdateHealthBar(_model);
    }
}