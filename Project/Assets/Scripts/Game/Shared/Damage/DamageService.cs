using System.Collections.Generic;
using Game.Minions.Data;
using Game.Shared.Damage.Components;
using UnityEngine;

namespace Game.Shared.Damage
{
    internal class DamageService
    {
        private readonly SpawnedUnits _spawnedUnits;
        private readonly Vector3 _attackPos;
        private readonly DamageProcessor _damageProcessor;

        public DamageService(SpawnedUnits spawnedUnits, Transform characterAttackPoint)
        {
            _spawnedUnits = spawnedUnits;
            _attackPos = characterAttackPoint.position;

            _damageProcessor = new DamageProcessor();
            _damageProcessor.OnKill += GainExp;
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

        private void GainExp()
        {
            Debug.Log("+exp");
        }

        public void MinionAttack(int damage)
        {
        }
    }
}