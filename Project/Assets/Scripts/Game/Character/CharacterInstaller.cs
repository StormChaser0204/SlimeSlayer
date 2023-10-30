using Common.Installers;
using Game.Character.Data;
using Game.Character.Handlers;
using Game.Character.Services;
using Game.Character.Signals;
using Game.Enemies.Signals;
using UnityEngine;

namespace Game.Character
{
    [System.Serializable]
    public class CharacterInstaller : InstallerBase
    {
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private BaseStatsData _baseStats;
        [SerializeField] private ExperienceData _experienceData;
        [SerializeField] private ExperienceForEnemyKillData _experienceForEnemyKillData;
        [SerializeField] private PowerUpData _powerUpData;

        protected override void BindServices()
        {
            var experienceMap = new ExperienceMap();

            foreach (var element in _experienceForEnemyKillData.Elements)
                experienceMap.Add(element.Type, element.Amount);

            Container.Bind<ExperienceMap>().FromInstance(experienceMap).AsSingle();

            Container.Bind<StatsInfo>().AsSingle()
                .WithArguments(_baseStats.Health, _baseStats.Damage, _baseStats.AttackRange,
                    _attackPosition.position,
                    _baseStats.InvulnerabilityDuration);

            Container.Bind<AbilitiesInfo>().AsSingle()
                .WithArguments(_baseStats.AttackCooldown, _baseStats.BlockCooldown);

            Container.BindInterfacesTo<CharacterMovementService>().AsSingle()
                .WithArguments(_baseStats.MovementSpeed);

            Container.BindInterfacesAndSelfTo<ActionService>().AsSingle();

            Container.Bind<ExperienceService>().AsSingle()
                .WithArguments(_experienceData.ExpForNewLevel, _experienceData.LevelScaler);

            Container.Bind<PowerUpService>().AsSingle()
                .WithArguments(_powerUpData);
        }

        protected override void BindHandlers()
        {
            Dispatcher.Bind().Handler<AttackHandler>().To<AttackSignal>();
            Dispatcher.Bind().Handler<BlockHandler>().To<BlockSignal>();
            Dispatcher.Bind().Handler<ExperienceHandler>().To<EnemyDiedSignal>();
            Dispatcher.Bind().Handler<PowerUpHandler>().To<SelectPowerUpSignal>();
        }
    }
}