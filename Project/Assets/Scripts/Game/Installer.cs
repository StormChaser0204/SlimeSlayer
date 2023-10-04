using Game.Character.Services;
using Game.Minions;
using Game.Minions.Data;
using Game.Minions.Services;
using Game.Shared.Damage;
using Game.Shared.Experience;
using UnityEngine;
using Zenject;

namespace Game
{
    [AddComponentMenu("Game/GameInstaller")]
    internal class Installer : MonoInstaller
    {
        [SerializeField] private float _baseCharacterSpeed;
        [SerializeField] private float _baseAttackCooldown;

        [SerializeField] private float _spawnTime;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private MinionView _testView;

        [SerializeField] private Transform _attackPosition;

        public override void InstallBindings()
        {
            InstallCharacter();
            InstallMinions();
            InstallOther();
        }

        private void InstallCharacter()
        {
            Container.BindInterfacesTo<CharacterMovementService>()
                .AsSingle()
                .WithArguments(_baseCharacterSpeed);
            Container.BindInterfacesAndSelfTo<AttackService>()
                .AsSingle()
                .WithArguments(_baseAttackCooldown);
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
        }

        private void InstallOther()
        {
            Container.Bind<DamageService>().AsSingle().WithArguments(_attackPosition);
            Container.Bind<ExperienceService>().AsSingle();
        }
    }
}