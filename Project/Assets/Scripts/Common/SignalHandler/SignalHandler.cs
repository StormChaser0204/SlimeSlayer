namespace Common.SignalHandler
{
    public abstract class SignalHandler : SignalHandlerBase, IHandler
    {
        public abstract void Handle();

        protected SignalHandler(ISignal ev) : base(ev)
        {
        }
    }
}