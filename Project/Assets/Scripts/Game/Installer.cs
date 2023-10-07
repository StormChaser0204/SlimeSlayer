using Game.Character.Experience;
using Game.Character.Services;
using Game.Minions;
using Game.Minions.Data;
using Game.Minions.Services;
using Game.Minions.Signals;
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
        [SerializeField] private View _testView;
        [Space] [SerializeField] private Transform _attackPosition;
        [Space] [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _healthBarParent;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            DeclareSignals();

            InstallCharacter();
            InstallMinions();
            InstallShared();
            InstallUI();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<TakerDiedSignal>();
            Container.DeclareSignal<HealthChangedSignal>();
            Container.DeclareSignal<SpawnMinionSignal>();
        }

        private void InstallCharacter()
        {
            Container.BindInterfacesTo<CharacterMovementService>()
                .AsSingle()
                .WithArguments(_baseCharacterSpeed);
            Container.BindInterfacesAndSelfTo<AttackService>()
                .AsSingle()
                .WithArguments(_baseAttackCooldown);

            Container.Bind<ExperienceService>().AsSingle();

            Container.BindSignal<TakerDiedSignal>()
                .ToMethod<ExperienceService>(x => x.AddExp).FromResolve();
        }

        private void InstallMinions()
        {
            Container.Bind<SpawnedUnits>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnService>()
                .AsSingle()
                .WithArguments(_spawnPoint, _spawnTime, _testView);
            Container.BindInterfacesAndSelfTo<MinionsMovementService>()
                .AsSingle()
                .WithArguments(_endPoint);

            Container.BindSignal<TakerDiedSignal>()
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

            Container.BindSignal<SpawnMinionSignal>()
                .ToMethod<HealthService>(x => x.InstantiateNewBar)
                .FromResolve();

            Container.BindSignal<TakerDiedSignal>()
                .ToMethod<HealthService>(x => x.ReturnBar)
                .FromResolve();

            Container.BindSignal<HealthChangedSignal>()
                .ToMethod<HealthService>(x => x.UpdateHealth)
                .FromResolve();
        }
    }
}