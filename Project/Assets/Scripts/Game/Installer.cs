using Game.Character.Experience;
using Game.Character.Services;
using Game.Character.Signals;
using Game.Enemies;
using Game.Enemies.Data;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using Game.Enemies.Spawn;
using Game.Shared.Damage;
using Game.Shared.Damage.Signals;
using Game.Shared.UI.Services;
using UnityEngine;
using Zenject;

namespace Game
{
    [AddComponentMenu("Game/GameInstaller")]
    internal class Installer : MonoInstaller
    {
        [SerializeField] private float _baseCharacterSpeed;
        [SerializeField] private float _baseAttackCooldown;

        [Space] [SerializeField] private float _spawnTime;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private View _enemyView;

        [Space] [SerializeField] private Transform _attackPosition;
        [Space] [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _healthBarParent;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            DeclareSignals();

            InstallCharacter();
            InstallEnemies();
            InstallShared();
            InstallUI();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<EnemyDiedSignal>();
            Container.DeclareSignal<HealthChangedSignal>();
            Container.DeclareSignal<SpawnEnemySignal>();

            Container.DeclareSignal<AttackSignal>();
            Container.DeclareSignal<BlockSignal>();
        }

        private void InstallCharacter()
        {
            Container.BindInterfacesTo<CharacterMovementService>().AsSingle()
                .WithArguments(_baseCharacterSpeed);

            Container.BindInterfacesAndSelfTo<AttackService>().AsSingle()
                .WithArguments(_baseAttackCooldown);

            Container.Bind<ExperienceService>().AsSingle();

            Container.BindSignal<AttackSignal>()
                .ToMethod<AttackService>(x => x.Attack)
                .FromResolve();
            Container.BindSignal<EnemyDiedSignal>()
                .ToMethod<ExperienceService>(x => x.AddExp)
                .FromResolve();
        }

        private void InstallEnemies()
        {
            Container.BindFactory<View, ViewFactory>().FromComponentInNewPrefab(_enemyView);
            Container.BindFactory<Model, Factory>()
                .FromPoolableMemoryPool(x => x.WithInitialSize(1).FromFactory<CustomFactory>());

            Container.Bind<ActiveEnemies>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnService>()
                .AsSingle()
                .WithArguments(_spawnPoint, _spawnTime);
            Container.BindInterfacesAndSelfTo<EnemiesMovementService>()
                .AsSingle()
                .WithArguments(_endPoint);

            Container.BindSignal<EnemyDiedSignal>()
                .ToMethod<SpawnService>(x => x.ReturnToPool)
                .FromResolve();
        }

        private void InstallShared()
        {
            Container.BindInterfacesAndSelfTo<DamageService>()
                .AsSingle()
                .WithArguments(_attackPosition);
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<HealthService>()
                .AsSingle()
                .WithArguments(_healthBarPrefab, _healthBarParent);

            Container.BindSignal<SpawnEnemySignal>()
                .ToMethod<HealthService>(x => x.InstantiateNewBar)
                .FromResolve();

            Container.BindSignal<EnemyDiedSignal>()
                .ToMethod<HealthService>(x => x.ReturnBar)
                .FromResolve();

            Container.BindSignal<HealthChangedSignal>()
                .ToMethod<HealthService>(x => x.UpdateHealthBar)
                .FromResolve();
        }
    }
}