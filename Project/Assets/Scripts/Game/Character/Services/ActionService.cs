using Common;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class ActionService : ITickable
    {
        private readonly DamageService _damageService;
        private readonly CharacterStats _characterStats;
        private readonly DelayedAction _block;

        private float _invulnerableDuration;

        public ActionService(DamageService damageService, CharacterStats stats)
        {
            _damageService = damageService;
            _characterStats = stats;
            _block = new DelayedAction(0, StopBlock);
        }

        public void Attack() => _damageService.Attack(_characterStats.Damage, 2);

        public void Block()
        {
            _characterStats.SetInvulnerableState(true);
            _block.SetDelay(_characterStats.InvulnerableDuration);
        }

        private void StopBlock() => _characterStats.SetInvulnerableState(false);

        public void IncreaseDamage() => _characterStats.UpdateDamage(2);

        public void Tick() => _block?.Update();
    }
}