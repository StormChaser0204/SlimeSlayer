using System.Linq;
using Common.SignalDispatching.Dispatcher;
using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Character.Handlers
{
    [UsedImplicitly]
    internal class AttackHandler : SignalHandler
    {
        [Inject] private readonly Data.StatsInfo _statsInfo;
        [Inject] private readonly ActiveEnemies _activeEnemies;
        [Inject] private readonly ISignalDispatcher _dispatcher;

        public AttackHandler(ISignal signal) : base(signal)
        {
        }

        public override void Handle()
        {
            var enemiesInRange = _activeEnemies.Where(InRange).ToArray();

            foreach (var enemy in enemiesInRange)
                DealDamage(_statsInfo.Damage, enemy);
        }

        private bool InRange(Model model)
        {
            var distance = Vector3.Distance(_statsInfo.AttackPosition, model.EnemyFacade.CurrentPosition);
            return distance <= _statsInfo.AttackRange;
        }

        private void DealDamage(float damage, Model model)
        {
            model.Health -= damage;
            RaiseSignal(new EnemyHealthChangedSignal(model));
            if (model.Health > 0)
                return;

            RaiseSignal(new EnemyDiedSignal(model));
        }

        private void RaiseSignal(ISignal signal) =>
            _dispatcher.Raise(signal);
    }
}