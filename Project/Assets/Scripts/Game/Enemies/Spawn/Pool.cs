using Game.Enemies.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Enemies.Spawn
{
    [UsedImplicitly]
    internal class Pool : PoolableMemoryPool<IMemoryPool, Model>
    {
        private readonly EnemyFacade _facade;

        public Pool(EnemyFacade facade) => _facade = facade;

        protected override void OnCreated(Model item) => item.SetView(Object.Instantiate(_facade));
    }
}