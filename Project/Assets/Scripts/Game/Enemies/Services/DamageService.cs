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

        private readonly Character.Data.CharacterInfo _characterInfo;
        private readonly InstancedEnemies _instancedEnemies;

        public DamageService(Character.Data.CharacterInfo characterInfo,
            InstancedEnemies instancedEnemies)
        {
            _characterInfo = characterInfo;
            _instancedEnemies = instancedEnemies;
        }

        public void Tick()
        {
            var readyToAttack = _instancedEnemies.Where(e => e.IsAlive).Where(e => e.EndPointReached);

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

            if (_characterInfo.IsInvulnerable)
                return;

            _characterInfo.UpdateHealth(-model.Damage);
            _dispatcher.Raise(new CharacterHealthChangedSignal(_characterInfo.TotalHealth,
                _characterInfo.CurrentHealth));
        }
    }
}