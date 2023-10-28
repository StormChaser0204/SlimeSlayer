using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Game.Common.GameState.Signals;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Common.GameState
{
    [UsedImplicitly]
    internal class TimeScaleHandler : SignalHandler
    {
        public TimeScaleHandler(ISignal signal) : base(signal)
        {
        }

        public override void Handle() => Time.timeScale = SignalIs<PauseGameSignal>() ? 0 : 1;
    }
}