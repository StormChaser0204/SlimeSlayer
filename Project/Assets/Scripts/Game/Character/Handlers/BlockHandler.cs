using System;
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
        [Inject] private StatsInfo _statsInfo;

        public BlockHandler(ISignal signal) : base(signal)
        {
        }

        public override async UniTask Handle(CancellationToken cancellationToken = default)
        {
            _statsInfo.SetInvulnerableActiveState(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_statsInfo.InvulnerableDuration),
                cancellationToken: cancellationToken);
            _statsInfo.SetInvulnerableActiveState(false);
        }
    }
}