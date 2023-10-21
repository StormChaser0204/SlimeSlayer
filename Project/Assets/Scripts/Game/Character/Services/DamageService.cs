using System;
using Game.Character.Signals;
using Game.Enemies.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Character.Services
{
    //move it to enemies?    
    [UsedImplicitly]
    internal class DamageService : IDisposable
    {
        private readonly ActiveEnemies _activeEnemies;
        private readonly Vector3 _attackPos;
        private readonly SignalBus _signalBus;

        public DamageService(ActiveEnemies activeEnemies, Transform characterAttackPoint,
            SignalBus signalBus)
        {
            _activeEnemies = activeEnemies;
            _attackPos = characterAttackPoint.position;
            _signalBus = signalBus;
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
     
        private void ChangeHealth(Model model) => _signalBus.Fire(new EnemyHealthChangedSignal(model));

        private void Death(Model model) => _signalBus.Fire(new EnemyDiedSignal(model));
        
        public void Dispose()
        {
        }
    }
}