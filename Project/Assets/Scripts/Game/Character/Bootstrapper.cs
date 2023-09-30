using Game.Character.Services;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Character
{
    [AddComponentMenu("Game/Character/Character Bootstrapper")]
    internal class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private float _baseCharacterSpeed;
        [SerializeField] private float _baseAttackCooldown;

        private ServiceLocator _locator;

        public void Start()
        {
            _locator = ServiceLocator.Instance;

            var movementService = new MovementService(_baseCharacterSpeed);
            var attackService = new AttackService(_baseAttackCooldown);

            _locator.Provide(movementService);
            _locator.Provide(attackService);
        }
    }
}