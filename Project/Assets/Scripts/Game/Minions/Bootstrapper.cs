using Game.Minions.Data;
using Game.Minions.Services;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Minions
{
    [AddComponentMenu("Game/Minions/Minions Bootstrapper")]
    internal class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private float _spawnTime;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private MinionView _testView;
        
        private ServiceLocator _locator;

        public void Start()
        {
            _locator = ServiceLocator.Instance;

            var spawnedUnits = new SpawnedUnits();
            var spawnService = new SpawnService(spawnedUnits, _spawnPoint, _spawnTime, _testView);
            var movementService = new MovementService(spawnedUnits, _endPoint);

            _locator.Provide(spawnedUnits);
            _locator.Provide(spawnService);
            _locator.Provide(movementService);
        }
    }
}