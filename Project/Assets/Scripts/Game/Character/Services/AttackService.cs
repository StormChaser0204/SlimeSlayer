using Common;
using Game.Shared.Damage;
using JetBrains.Annotations;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class AttackService
    {
        private readonly DamageService _damageService;
        private readonly DamageDealer _damageDealer;
        private readonly RepeatableAction _attack;

        public AttackService(DamageService damageService, float baseAttackCooldown)
        {
            _damageService = damageService;
            _damageDealer = new DamageDealer(1);
        }

        public void Attack() => _damageService.CharacterAttack(_damageDealer, 2);
    }
}