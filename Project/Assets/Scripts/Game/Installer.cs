using Game.Character.Services;
using Game.Character.Signals;
using Game.Enemies;
using Game.Enemies.Data;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using Game.Enemies.Spawn;
using Game.Shared.Damage;
using Game.Shared.Damage.Signals;
using Game.UI;
using Game.UI.Health;
using UnityEngine;
using Zenject;

namespace Game
{
    [AddComponentMenu("Game/GameInstaller")]
    internal class Installer : MonoInstaller
    {
        [SerializeField] private float _baseCharacterSpeed;
        [SerializeField] private float _baseAttackCooldown;
        [SerializeField] private int _nextLevelExp;
        [SerializeField] private int _levelScaler;

        [Space] [SerializeField] private float _spawnTime;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private View _enemyView;

        [Space] [SerializeField] private Transform _attackPosition;
        [Space] [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _healthBarParent;
        [SerializeField] private CharacterPanel _characterPanel;

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
            Container.DeclareSignal<AttackSignal>();
            Container.DeclareSignal<BlockSignal>();
            Container.DeclareSignal<ExperienceChangedSignal>();
            Container.DeclareSignal<LevelUpSignal>();

            Container.DeclareSignal<EnemyDiedSignal>();
            Container.DeclareSignal<HealthChangedSignal>();
            Container.DeclareSignal<SpawnEnemySignal>();
        }

        private void InstallCharacter()
        {
            Container.BindInterfacesTo<CharacterMovementService>().AsSingle()
                .WithArguments(_baseCharacterSpeed);

            Container.BindInterfacesAndSelfTo<AttackService>().AsSingle()
                .WithArguments(_baseAttackCooldown);

            Container.Bind<ExperienceService>().AsSingle()
                .WithArguments(_nextLevelExp, _levelScaler);

            Container.BindSignal<AttackSignal>()
                .ToMethod<AttackService>(x => x.Attack)
                .FromResolve();

            Container.BindSignal<LevelUpSignal>()
                .ToMethod<AttackService>(x => x.IncreaseDamage)
                .FromResolve();

            Container.BindSignal<EnemyDiedSignal>()
                .ToMethod<ExperienceService>(x => x.AddExperience)
                .FromResolve();
        }

        private void InstallEnemies()
        {
            Container.BindFactory<View, ViewFactory>().FromComponentInNewPrefab(_enemyView);
            Container.BindFactory<Model, Factory>()
                .FromPoolableMemoryPool(x => x.WithInitialSize(1).FromFactory<CustomFactory>());

            Container.Bind<ActiveEnemies>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnService>().AsSingle()
                .WithArguments(_spawnPoint, _spawnTime);
            Container.BindInterfacesAndSelfTo<EnemiesMovementService>().AsSingle()
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
            Container.BindInstance(_characterPanel);
            _characterPanel.Init(0, _nextLevelExp);
            
            Container.BindInterfacesAndSelfTo<HealthService>().AsSingle()
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

            Container.BindSignal<LevelUpSignal>()
                .ToMethod(x => _characterPanel.SetNewLevelProgress(x));

            Container.BindSignal<ExperienceChangedSignal>()
                .ToMethod(x => _characterPanel.UpdateProgress(x));
        }
    }
}