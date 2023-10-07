using System;
using Common;
using Game.Minions.Data;
using Game.Minions.Signals;
using Game.Shared.Damage.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Minions.Services
{
    [UsedImplicitly]
    internal class SpawnService : ITickable, IDisposable
    {
        private readonly SpawnedUnits _spawnedUnits;
        private readonly Vector3 _spawnPoint;
        private readonly View _minionView;
        private readonly RepeatableAction _spawn;
        private readonly MinionPool _pool;
        private readonly SignalBus _signalBus;

        public SpawnService(SpawnedUnits spawnedUnits, Transform spawnPoint, float spawnTime,
            View minionView, SignalBus signalBus)
        {
            _spawnedUnits = spawnedUnits;
            _spawn = new RepeatableAction(spawnTime, SpawnUnit);
            _pool = new MinionPool(minionView, spawnPoint, _spawnedUnits);
            _signalBus = signalBus;
        }

        public void Tick() => _spawn.Update();

        private void SpawnUnit()
        {
            var model = _pool.GetItem();
            _signalBus.Fire(new SpawnMinionSignal(model));
        }

        public void ReturnToPool(TakerDiedSignal signal) => _pool.ReturnToPool(signal.Taker);

        public void Dispose()
        {
        }
    }
}