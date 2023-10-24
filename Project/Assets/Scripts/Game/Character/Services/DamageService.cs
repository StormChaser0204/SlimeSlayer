using Common.SignalDispatching.Dispatcher;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class DamageService
    {
        [Inject] private ISignalDispatcher _dispatcher;

        private readonly ActiveEnemies _activeEnemies;
        private readonly Vector3 _attackPos;

        public DamageService(ActiveEnemies activeEnemies, Transform characterAttackPoint)
        {
            _activeEnemies = activeEnemies;
            _attackPos = characterAttackPoint.position;
        }

        public void Attack(float damage, float range)
        {
            for (var i = 0; i < _activeEnemies.Count; i++)
            {
                var enemy = _activeEnemies[i];
                var distance = Vector3.Distance(_attackPos, enemy.View.CurrentPosition);
                if (distance > range)
                    continue;

                DealDamage(damage, enemy);
            }
        }

        private void DealDamage(float damage, Model model)
        {
            model.Health -= damage;
            ChangeHealth(model);
            if (model.Health > 0)
                return;

            Death(model);
        }

        private void ChangeHealth(Model model) =>
            _dispatcher.Raise(new EnemyHealthChangedSignal(model));

        private void Death(Model model) => _dispatcher.Raise(new EnemyDiedSignal(model));
    }
}