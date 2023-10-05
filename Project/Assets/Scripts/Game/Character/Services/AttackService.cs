using Common;
using Game.Shared.Damage;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class AttackService : ITickable
    {
        private readonly DamageService _damageService;
        private readonly DamageDealer _damageDealer;
        private readonly RepeatableAction _attack;

        public AttackService(DamageService damageService, float baseAttackCooldown)
        {
            _damageService = damageService;
            _damageDealer = new DamageDealer(1);
            _attack = new RepeatableAction(baseAttackCooldown, Attack);
        }

        public void Tick() => _attack.Update();

        private void Attack() => _damageService.CharacterAttack(_damageDealer, 2);
    }
}