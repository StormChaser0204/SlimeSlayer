using System.Linq;
using Game.Character;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Enemies.Services
{
    [UsedImplicitly]
    internal class DamageService : ITickable
    {
        private readonly CharacterStats _characterStats;
        private readonly ActiveEnemies _activeEnemies;
        private readonly SignalBus _signalBus;

        public DamageService(CharacterStats characterStats, ActiveEnemies activeEnemies,
            SignalBus signalBus)
        {
            _characterStats = characterStats;
            _activeEnemies = activeEnemies;
            _signalBus = signalBus;
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
            _characterStats.UpdateHealth(-model.Damage);
            model.ResetAttackCooldown();
            _signalBus.Fire(new CharacterHealthChangedSignal(_characterStats.TotalHealth,
                _characterStats.CurrentHealth));
        }
    }
}