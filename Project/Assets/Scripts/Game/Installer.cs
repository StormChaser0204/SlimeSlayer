using Common.SignalDispatching.Dispatcher;
using Game.Character;
using Game.Character.Services;
using Game.Character.Signals;
using Game.Enemies;
using Game.Enemies.Data;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using Game.Enemies.Spawn;
using Game.UI;
using Game.UI.Health;
using UnityEngine;
using Zenject;

namespace Game
{
    [AddComponentMenu("Game/GameInstaller")]
    internal class Installer : MonoInstaller
    {
        [SerializeField] private int _baseCharacterHealth;
        [SerializeField] private float _baseCharacterDamage;
        [SerializeField] private float _baseCharacterInvulnerableDuration;
        [SerializeField] private float _baseCharacterSpeed;
        [SerializeField] private float _baseAttackCooldown;
        [SerializeField] private float _baseBlockCooldown;
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
        [SerializeField] private Camera _mainCamera;

        private ISignalDispatcher _dispatcher;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);


            DeclareSignals();

            InstallCommon();

            InstallCharacter();
            InstallEnemies();
            InstallUI();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<AttackSignal>();
            Container.DeclareSignal<BlockSignal>();
            Container.DeclareSignal<ExperienceChangedSignal>();
            Container.DeclareSignal<LevelUpSignal>();
            Container.DeclareSignal<CharacterHealthChangedSignal>();
            Container.DeclareSignal<CharacterDiedSignal>();

            Container.DeclareSignal<EnemyDiedSignal>();
            Container.DeclareSignal<EnemyHealthChangedSignal>();
            Container.DeclareSignal<SpawnEnemySignal>();
        }

        private void InstallCommon()
        {
            _dispatcher = new SignalDispatcher(Container);
            Container.Bind<ISignalDispatcher>().FromInstance(_dispatcher).AsSingle();
        }

        private void InstallCharacter()
        {
            Container.Bind<Character.CharacterInfo>().AsSingle()
                .WithArguments(_baseCharacterHealth, _baseCharacterDamage,
                    _baseCharacterInvulnerableDuration);

            Container.Bind<AbilitiesInfo>().AsSingle()
                .WithArguments(_baseCharacterHealth, _baseCharacterDamage,
                    _baseCharacterInvulnerableDuration);

            Container.BindInterfacesTo<CharacterMovementService>().AsSingle()
                .WithArguments(_baseCharacterSpeed);

            Container.BindInterfacesAndSelfTo<ActionService>().AsSingle();

            Container.Bind<ExperienceService>().AsSingle()
                .WithArguments(_nextLevelExp, _levelScaler);

            Container.Bind<Character.Services.DamageService>()
                .AsSingle()
                .WithArguments(_attackPosition);

            Container.BindSignal<AttackSignal>()
                .ToMethod<ActionService>(x => x.Attack)
                .FromResolve();

            Container.BindSignal<BlockSignal>()
                .ToMethod<ActionService>(x => x.Block)
                .FromResolve();

            Container.BindSignal<LevelUpSignal>()
                .ToMethod<ActionService>(x => x.IncreaseDamage)
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

            Container.BindInterfacesTo<Enemies.Services.DamageService>().AsSingle();
        }

        private void InstallUI()
        {
            Container.BindInstance(_characterPanel);
            _characterPanel.Init(0, _nextLevelExp, _mainCamera);

            Container.BindInterfacesAndSelfTo<HealthService>().AsSingle()
                .WithArguments(_healthBarPrefab, _healthBarParent, _mainCamera);

            Container.BindSignal<SpawnEnemySignal>()
                .ToMethod<HealthService>(x => x.InstantiateNewBar)
                .FromResolve();

            Container.BindSignal<EnemyDiedSignal>()
                .ToMethod<HealthService>(x => x.ReturnBar)
                .FromResolve();

            Container.BindSignal<EnemyHealthChangedSignal>()
                .ToMethod<HealthService>(x => x.UpdateHealthBar)
                .FromResolve();

            Container.BindSignal<LevelUpSignal>()
                .ToMethod(x => _characterPanel.SetNewLevelProgress(x));

            Container.BindSignal<ExperienceChangedSignal>()
                .ToMethod(x => _characterPanel.UpdateProgress(x));

            Container.BindSignal<CharacterHealthChangedSignal>()
                .ToMethod(x => _characterPanel.UpdateHealth(x));
        }
    }
}