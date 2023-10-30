using System.Linq;
using Common.SignalDispatching.Dispatcher;
using Game.Character.Signals;
using Game.Enemies.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Enemies.Services
{
    [UsedImplicitly]
    internal class DamageService : ITickable
    {
        [Inject] private ISignalDispatcher _dispatcher;

        private readonly Character.Data.StatsInfo _statsInfo;
        private readonly ActiveEnemies _activeEnemies;

        public DamageService(Character.Data.StatsInfo statsInfo,
            ActiveEnemies activeEnemies)
        {
            _statsInfo = statsInfo;
            _activeEnemies = activeEnemies;
        }

        public void Tick()
        {
            var readyToAttack = _activeEnemies.Where(e => e.EndPointReached);

            foreach (var enemy in readyToAttack)
            {
                if (enemy.AttackCooldown > 0)
                    enemy.AttackCooldown -= Time.deltaTime;
                else
                    DealDamage(enemy);
            }
        }

        private void DealDamage(Model model)
        {
            model.ResetAttackCooldown();

            if (_statsInfo.IsInvulnerable)
                return;

            _statsInfo.UpdateHealth(-model.Damage);
            _dispatcher.Raise(new CharacterHealthChangedSignal(_statsInfo.TotalHealth,
                _statsInfo.CurrentHealth));
        }
    }
}