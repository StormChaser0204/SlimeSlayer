using System.Collections.Generic;
using System.Linq;
using Game.Minions.Data;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Character.Services.Depricated
{
    //internal class DamageService : ITickable
    //{
    //    private readonly float _minHitDistance;
    //    private readonly float _range;
    //    private readonly SpawnedUnits _spawnedUnits;
    //
    //    private readonly float _baseCooldown;
    //    private readonly float _damage;
    //
    //    private float _cooldown;
    //    private bool _isReadyToAttack;
    //
    //    public DamageService(float minHitDistance, float range, SpawnedUnits spawnedUnits, float cooldown, float damage)
    //    {
    //        _range = range;
    //        _spawnedUnits = spawnedUnits;
    //        _minHitDistance = minHitDistance;
    //        _baseCooldown = cooldown;
    //        _damage = damage;
    //        _isReadyToAttack = true;
    //    }
    //
    //    public void Tick()
    //    {
    //        if (_isReadyToAttack)
    //            ProceedAttack();
    //        else
    //            UpdateCooldown();
    //    }
    //
    //    private void ProceedAttack()
    //    {
    //        _isReadyToAttack = false;
    //        var targets = GetTargetsInRange();
    //        foreach (var target in targets)
    //        {
    //            var model = target.MinionModel;
    //
    //            model.Health -= _damage;
    //            if (model.Health <= 0)
    //                ProceedTargetDead(model);
    //        }
    //    }
    //
    //    private void ProceedTargetDead(MinionModel minionModel)
    //    {
    //        Debug.Log("Dead");
    //        //back to pool;
    //    }
    //
    //    private void UpdateCooldown()
    //    {
    //        if (_cooldown > 0)
    //        {
    //            _cooldown -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            _cooldown = _baseCooldown;
    //            _isReadyToAttack = true;
    //        }
    //    }
    //
    //    private IEnumerable<MinionModel> GetTargetsInRange() =>
    //        _spawnedUnits.Where(u => u.transform.position.x < _minHitDistance + _range);
    //}
}