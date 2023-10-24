using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Game.Character.Data;
using Game.Character.Services;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Handlers
{
    [UsedImplicitly]
    internal class AttackHandler : SignalHandler
    {
        [Inject] private readonly DamageService _damageService;
        [Inject] private readonly CharacterInfo _characterInfo;

        public AttackHandler(ISignal signal) : base(signal)
        {
        }

        public override void Handle() =>
            _damageService.Attack(_characterInfo.Damage, _characterInfo.AttackRange);
    }
}