using System.Threading;
using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Cysharp.Threading.Tasks;
using Game.Character.Data;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Handlers
{
    [UsedImplicitly]
    internal class BlockHandler : TaskSignalHandler
    {
        [Inject] private CharacterInfo _characterInfo;

        public BlockHandler(ISignal signal) : base(signal)
        {
        }

        public override async UniTask Handle(CancellationToken cancellationToken = default)
        {
            _characterInfo.SetInvulnerableActiveState(true);
            await UniTask.WaitForSeconds(_characterInfo.InvulnerableDuration,
                cancellationToken: cancellationToken);
            _characterInfo.SetInvulnerableActiveState(false);
        }
    }
}