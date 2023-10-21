using Common;
using JetBrains.Annotations;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class AttackService
    {
        private readonly DamageService _damageService;
        private readonly CharacterStats _characterStats;
        private readonly RepeatableAction _attack;

        public AttackService(DamageService damageService, CharacterStats stats,
            float baseAttackCooldown)
        {
            _damageService = damageService;
            _characterStats = stats;
        }

        public void Attack() => _damageService.Attack(_characterStats.Damage, 2);

        public void IncreaseDamage() => _characterStats.UpdateDamage(2);
    }
}