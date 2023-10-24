using Common.SignalDispatching.Dispatcher;
using Game.Character.Data;
using Game.Character.Handlers;
using Game.Character.Services;
using Game.Character.Signals;
using Game.Enemies;
using Game.Enemies.Data;
using Game.Enemies.Handlers;
using Game.Enemies.Services;
using Game.Enemies.Signals;
using Game.Enemies.Spawn;
using Game.UI;
using Game.UI.Handlers;
using Game.UI.Health;
using UnityEngine;
using Zenject;

namespace Game
{
    [AddComponentMenu("Game/GameInstaller")]
    internal class Installer : MonoInstaller
    {
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private int _baseCharacterHealth;
        [SerializeField] private float _baseCharacterDamage;
        [SerializeField] private float _baseCharacterInvulnerableDuration;
        [SerializeField] private float _baseCharacterSpeed;
        [SerializeField] private float _baseAttackCooldown;
        [SerializeField] private float _baseBlockCooldown;
        [SerializeField] private float _baseAttackRange;
        [SerializeField] private int _nextLevelExp;
        [SerializeField] private int _levelScaler;
        [SerializeField] private ExperienceData _experienceData;

        [Space] [SerializeField] private float _spawnTime;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private View _enemyView;

        [Space] [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _healthBarParent;
        [SerializeField] private CharacterPanel _characterPanel;
        [SerializeField] private Camera _mainCamera;

        private ISignalDispatcher _dispatcher;

        public override void InstallBindings()
        {
            _dispatcher = new SignalDispatcher(Container);
            Container.Bind<ISignalDispatcher>().FromInstance(_dispatcher).AsSingle();

            InstallCharacter();
            InstallEnemies();
            InstallUI();
        }

        private void InstallCharacter()
        {
            var experienceMap = new ExperienceMap();

            foreach (var element in _experienceData.Elements)
                experienceMap.Add(element.Type, element.Amount);

            Container.Bind<ExperienceMap>().FromInstance(experienceMap);

            Container.Bind<Character.Data.CharacterInfo>().AsSingle()
                .WithArguments(_baseCharacterHealth, _baseCharacterDamage, _baseAttackRange,
                    _attackPosition.position,
                    _baseCharacterInvulnerableDuration);

            Container.Bind<AbilitiesInfo>().AsSingle()
                .WithArguments(_baseAttackCooldown, _baseBlockCooldown);

            Container.BindInterfacesTo<CharacterMovementService>().AsSingle()
                .WithArguments(_baseCharacterSpeed);

            Container.BindInterfacesAndSelfTo<ActionService>().AsSingle();

            Container.Bind<ExperienceService>().AsSingle()
                .WithArguments(_nextLevelExp, _levelScaler);

            _dispatcher.Bind().Handler<AttackHandler>().To<AttackSignal>();
            _dispatcher.Bind().Handler<BlockHandler>().To<BlockSignal>();
            _dispatcher.Bind().Handler<ExperienceHandler>().To<EnemyDiedSignal>();
            _dispatcher.Bind().Handler<PowerUpHandler>().To<LevelUpSignal>();
        }

        private void InstallEnemies()
        {
            Container.BindFactory<View, ViewFactory>().FromComponentInNewPrefab(_enemyView);
            Container.BindFactory<Model, Factory>()
                .FromPoolableMemoryPool(x => x.WithInitialSize(1).FromFactory<CustomFactory>());

            Container.Bind<InstancedEnemies>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnService>().AsSingle()
                .WithArguments(_spawnPoint, _spawnTime);
            Container.BindInterfacesAndSelfTo<EnemiesMovementService>().AsSingle()
                .WithArguments(_endPoint);

            Container.BindInterfacesTo<DamageService>().AsSingle();

            _dispatcher.Bind().Handler<PoolHandler>().To<EnemyDiedSignal>();
        }

        private void InstallUI()
        {
            Container.BindInstance(_characterPanel);
            _characterPanel.Init(0, _nextLevelExp, _mainCamera);

            Container.BindInterfacesAndSelfTo<HealthService>().AsSingle()
                .WithArguments(_healthBarPrefab, _healthBarParent, _mainCamera);

            _dispatcher.Bind().Handler<EnemyHealthBarHandler>().To<SpawnEnemySignal>();
            _dispatcher.Bind().Handler<EnemyHealthBarHandler>().To<EnemyDiedSignal>();
            _dispatcher.Bind().Handler<EnemyHealthBarHandler>().To<EnemyHealthChangedSignal>();

            _dispatcher.Bind().Handler<CharacterNewLevelHandler>().To<LevelUpSignal>();
            _dispatcher.Bind().Handler<CharacterLevelProgressHandler>().To<ExperienceChangedSignal>();
            _dispatcher.Bind().Handler<CharacterHealthHandler>().To<CharacterHealthChangedSignal>();
        }
    }
}