using System;
using Common;
using Game.Minions.Data;
using UnityEngine;
using Zenject;

namespace Game.Minions.Services
{
    internal class SpawnService : ITickable, IDisposable
    {
        private readonly SpawnedUnits _spawnedUnits;
        private readonly Vector3 _spawnPoint;

        private readonly MinionView _minionView;
        private readonly RepeatableAction _spawn;
        private readonly MinionPool _pool;

        public SpawnService(SpawnedUnits spawnedUnits, Transform spawnPoint, float spawnTime,
            MinionView minionView)
        {
            _spawnedUnits = spawnedUnits;
            _spawn = new RepeatableAction(spawnTime, SpawnUnit);
            _pool = new MinionPool(minionView, spawnPoint, _spawnedUnits);
        }

        public void Tick() => _spawn.Update();

        private void SpawnUnit() => _pool.GetItem();

        public void Dispose()
        {
        }
    }
}