using Common;
using Game.Shared.Damage;
using JetBrains.Annotations;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class AttackService
    {
        private readonly DamageService _damageService;
        private readonly Stats _stats;
        private readonly RepeatableAction _attack;

        public AttackService(DamageService damageService, float baseAttackCooldown)
        {
            _damageService = damageService;
            _stats = new Stats(1);
        }

        public void Attack() => _damageService.CharacterAttack(_stats.Damage, 2);

        public void IncreaseDamage() => _stats.UpdateDamage(2);
    }
}