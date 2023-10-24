namespace Common.SignalHandler.Handlers
{
    public abstract class SignalHandler : SignalHandlerBase, IHandler
    {
        public abstract void Handle();

        protected SignalHandler(ISignal signal) : base(signal)
        {
        }
    }
}