using System;
using Common;
using Common.SignalDispatching.Dispatcher;
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
        [Inject] private ISignalDispatcher _dispatcher;

        private readonly ActiveEnemies _activeEnemies;
        private readonly Factory _factory;
        private readonly Vector3 _spawnPoint;
        private readonly RepeatableAction _spawn;

        public SpawnService(ActiveEnemies activeEnemies, Factory factory,
            Transform spawnPoint, float spawnTime)
        {
            _activeEnemies = activeEnemies;
            _factory = factory;
            _spawnPoint = spawnPoint.position;
            _spawn = new RepeatableAction(spawnTime, SpawnUnit);
        }

        public void Tick() => _spawn.Update();

        private void SpawnUnit()
        {
            var model = _factory.Create();
            model.SetViewActiveState(true);
            model.View.transform.position = _spawnPoint;
            model.Init(Data.Type.Small, 1, 3, 2, 2);
            _activeEnemies.Add(model);
            _dispatcher.Raise(new SpawnEnemySignal(model));
        }

        public void ReturnToPool(Model model)
        {
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