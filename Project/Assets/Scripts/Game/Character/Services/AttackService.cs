using Common;
using Game.Shared.Damage;
using Game.Shared.Services;

namespace Game.Character.Services
{
    internal class AttackService : ITickable
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static DamageService DamageService => Locator.Get<DamageService>();

        private readonly DamageDealer _damageDealer;
        private readonly RepeatableAction _attack;
        
        public AttackService(float baseAttackCooldown)
        {
            _damageDealer = new DamageDealer(1);
            _attack = new RepeatableAction(baseAttackCooldown, Attack);
        }

        public void Tick() => _attack.Update();

        private void Attack() => DamageService.CharacterAttack(_damageDealer, 2);
    }
}