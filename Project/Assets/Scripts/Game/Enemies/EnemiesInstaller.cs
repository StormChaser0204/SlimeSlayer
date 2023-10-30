using Game.Enemies.Data;
using Game.Enemies.Handlers;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using Game.Enemies.Spawn;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Enemies
{
    [System.Serializable]
    public class EnemiesInstaller : global::Common.Installers.InstallerBase
    {
        [SerializeField] private float _spawnTime;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private EnemyFacade _enemyFacade;

        protected override void BindServices()
        {
            Container.BindFactory<Model, Factory>()
                .FromPoolableMemoryPool<Model, Pool>(x => x.WithFactoryArguments(_enemyFacade));
            Container.Bind<ActiveEnemies>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnService>().AsSingle()
                .WithArguments(_spawnPoint, _spawnTime);
            Container.BindInterfacesAndSelfTo<EnemiesMovementService>().AsSingle()
                .WithArguments(_endPoint);

            Container.BindInterfacesTo<DamageService>().AsSingle();
        }

        protected override void BindHandlers()
        {
            Dispatcher.Bind().Handler<ActiveEnemiesHandler>().To<EnemyDiedSignal>();
            Dispatcher.Bind().Handler<ActiveEnemiesHandler>().To<SpawnEnemySignal>();
        }
    }
}