using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using JetBrains.Annotations;

namespace Game.Character.Handlers
{
    [UsedImplicitly]
    internal class PowerUpHandler : SignalHandler
    {
        public PowerUpHandler(ISignal signal) : base(signal)
        {
        }

        public override void Handle()
        {
            //show power up dialog
            ////wait until power up selected
            //process selected power up
        }
    }
}