using Common;
using Game.Minions.Data;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Minions.Services
{
    internal class SpawnService : ITickable
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
    }
}