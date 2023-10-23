namespace Common.SignalHandler
{
    public abstract class SignalHandlerBase
    {
        private readonly ISignal _signal;

        protected SignalHandlerBase(ISignal signal) => _signal = signal;

        protected T SignalAs<T>() where T : class => _signal as T;

        protected bool SignalIs<T>() => _signal is T;
    }
}