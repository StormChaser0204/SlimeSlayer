using System;
using System.Collections.Generic;
using Game.Minions.Data;
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
        private readonly SpawnedUnits _spawnedUnits;
        private readonly Vector3 _attackPos;
        private readonly DamageProcessor _damageProcessor;
        private readonly SignalBus _signalBus;

        public DamageService(SpawnedUnits spawnedUnits, Transform characterAttackPoint,
            SignalBus signalBus)
        {
            _spawnedUnits = spawnedUnits;
            _attackPos = characterAttackPoint.position;

            _damageProcessor = new DamageProcessor();
            _damageProcessor.OnHealthChanged += ChangeHealth;
            _damageProcessor.OnDeath += Death;

            _signalBus = signalBus;
        }

        public void CharacterAttack(IDamageDealer damageDealer, float range)
        {
            var targets = new List<MinionModel>(_spawnedUnits);

            foreach (var unit in targets)
            {
                var distance = Vector3.Distance(_attackPos, unit.View.CurrentPosition);
                if (distance > range)
                    continue;

                _damageProcessor.Process(damageDealer, unit.DamageTaker);
            }
        }

        public void MinionAttack(int damage)
        {
        }

        private void ChangeHealth(IDamageTaker taker)
        {
            //fire health changed signal

            _signalBus.Fire(new HealthChangedSignal(taker));
            //на него подпишеться "UI Service" который будет обновлять вью ХП
        }

        private void Death(IDamageTaker taker) => _signalBus.Fire(new TakerDiedSignal(taker));

        public void Dispose()
        {
        }
    }
}