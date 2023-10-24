﻿using System.Linq;
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
        private readonly ActiveEnemies _activeEnemies;

        public DamageService(Character.Data.CharacterInfo characterInfo, ActiveEnemies activeEnemies)
        {
            _characterInfo = characterInfo;
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
            if (_characterInfo.IsInvulnerable)
                return;

            _characterInfo.UpdateHealth(-model.Damage);
            model.ResetAttackCooldown();
            _dispatcher.Raise(new CharacterHealthChangedSignal(_characterInfo.TotalHealth,
                _characterInfo.CurrentHealth));
        }
    }
}