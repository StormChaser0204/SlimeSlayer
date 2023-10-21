using System;
using Common;
using Game.Character.Signals;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using Game.Enemies.Spawn;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Enemies.Services
{
    [UsedImplicitly]
    internal class SpawnService : ITickable, IDisposable
    {
        private readonly ActiveEnemies _activeEnemies;
        private readonly Factory _factory;
        private readonly Vector3 _spawnPoint;
        private readonly RepeatableAction _spawn;
        private readonly SignalBus _signalBus;

        public SpawnService(ActiveEnemies activeEnemies, Factory factory,
            Transform spawnPoint, float spawnTime, SignalBus signalBus)
        {
            _activeEnemies = activeEnemies;
            _factory = factory;
            _spawnPoint = spawnPoint.position;
            _spawn = new RepeatableAction(spawnTime, SpawnUnit);
            _signalBus = signalBus;
        }

        public void Tick() => _spawn.Update();

        private void SpawnUnit()
        {
            var model = _factory.Create();
            model.SetViewActiveState(true);
            model.View.transform.position = _spawnPoint;
            model.Init(Data.Type.Small, 1, 3, 2, 2);
            _activeEnemies.Add(model);
            _signalBus.Fire(new SpawnEnemySignal(model));
        }

        public void ReturnToPool(EnemyDiedSignal signal)
        {
            var model = signal.Model;
            model.SetViewActiveState(false);
            model.Dispose();
            _activeEnemies.Remove(model);
        }

        public void Dispose()
        {
            //dispose all active enemies
        }
    }
}