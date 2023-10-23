using System.Linq;
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
        private readonly Character.CharacterInfo _characterInfo;
        private readonly ActiveEnemies _activeEnemies;
        private readonly SignalBus _signalBus;

        public DamageService(Character.CharacterInfo characterInfo, ActiveEnemies activeEnemies,
            SignalBus signalBus)
        {
            _characterInfo = characterInfo;
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
            if (_characterInfo.IsInvulnerable)
                return;

            _characterInfo.UpdateHealth(-model.Damage);
            model.ResetAttackCooldown();
            _signalBus.Fire(new CharacterHealthChangedSignal(_characterInfo.TotalHealth,
                _characterInfo.CurrentHealth));
        }
    }
}