using Game.Minions.Data;
using Game.Shared.Damage;
using Game.Shared.Experience;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Shared
{
    [AddComponentMenu("Game/Share/Share Bootstrapper")]
    internal class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _attackPosition;

        private ServiceLocator _serviceLocator;
        private SpawnedUnits _spawnedUnits;
        private TickableSerivceController _tickableSerivceController;

        public void Start()
        {
            _serviceLocator = ServiceLocator.Instance;
            SetupTickableServiceController();
            SetupDamage();
        }

        private void SetupTickableServiceController()
        {
            var go = new GameObject("TickableSerivceController");
            _tickableSerivceController = go.AddComponent<TickableSerivceController>();
            _serviceLocator.OnNewServiceAdded += _tickableSerivceController.CheckAndAddService;
        }

        private void SetupDamage()
        {
            _serviceLocator.Provide(new DamageService(_attackPosition));
            _serviceLocator.Provide(new ExperienceService());
        }
    }
}