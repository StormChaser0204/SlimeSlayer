using System.Collections.Generic;
using Game.Minions.Data;
using Game.Shared.Damage.Components;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Shared.Damage
{
    public class DamageService
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static SpawnedUnits SpawnedUnits => Locator.Get<SpawnedUnits>();

        private readonly DamageProcessor _damageProcessor;
        private readonly Vector3 _attackPos;

        public DamageService(Transform characterAttackPoint)
        {
            _damageProcessor = new DamageProcessor();
            _damageProcessor.OnKill += GainExp;
            _attackPos = characterAttackPoint.position;
        }

        public void CharacterAttack(IDamageDealer damageDealer, float range)
        {
            var targets = new List<MinionModel>(SpawnedUnits);

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