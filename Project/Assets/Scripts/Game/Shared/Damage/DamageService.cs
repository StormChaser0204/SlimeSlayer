using System;
using Game.Enemies.Data;
using Game.Shared.Damage.Components;
using Game.Shared.Damage.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Shared.Damage
{
    [UsedImplicitly]
    internal class DamageService : IDisposable
    {
        private readonly ActiveEnemies _activeEnemies;
        private readonly Vector3 _attackPos;
        private readonly DamageProcessor _damageProcessor;
        private readonly SignalBus _signalBus;

        public DamageService(ActiveEnemies activeEnemies, Transform characterAttackPoint,
            SignalBus signalBus)
        {
            _activeEnemies = activeEnemies;
            _attackPos = characterAttackPoint.position;

            _damageProcessor = new DamageProcessor();
            _damageProcessor.OnHealthChanged += ChangeHealth;
            _damageProcessor.OnDeath += Death;

            _signalBus = signalBus;
        }

        public void CharacterAttack(IDamageDealer damageDealer, float range)
        {
            for (var i = 0; i < _activeEnemies.Count; i++)
            {
                var enemy = _activeEnemies[i];
                var distance = Vector3.Distance(_attackPos, enemy.View.CurrentPosition);
                if (distance > range)
                    continue;

                _damageProcessor.Process(damageDealer.Damage, enemy);
            }
        }

        public void MinionAttack(int damage)
        {
        }

        private void ChangeHealth(Model model)
        {
            //fire health changed signal

            _signalBus.Fire(new HealthChangedSignal(model));
            //на него подпишеться "UI Service" который будет обновлять вью ХП
        }

        private void Death(Model model) => _signalBus.Fire(new EnemyDiedSignal(model));

        public void Dispose()
        {
        }
    }
}