using Common;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class ActionService : ITickable
    {
        private readonly DamageService _damageService;
        private readonly CharacterInfo _characterInfo;
        private readonly DelayedAction _block;

        private float _invulnerableDuration;

        public ActionService(DamageService damageService, CharacterInfo info)
        {
            _damageService = damageService;
            _characterInfo = info;
            _block = new DelayedAction(0, StopBlock);
        }

        public void Attack() => _damageService.Attack(_characterInfo.Damage, 2);

        public void Block()
        {
            _characterInfo.SetInvulnerableState(true);
            _block.SetDelay(_characterInfo.InvulnerableDuration);
        }

        private void StopBlock() => _characterInfo.SetInvulnerableState(false);

        public void IncreaseDamage() => _characterInfo.UpdateDamage(2);

        public void Tick() => _block?.Update();
    }
}