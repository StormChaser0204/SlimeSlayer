using Common.SignalDispatching.Binding;
using Common.SignalHandler;
using Common.SignalHandler.Handlers;

namespace Common.SignalDispatching.Dispatcher
{
    public interface ISignalDispatcher
    {
        Binder<SignalHandlerBase> Bind();
        void Raise(ISignal signal);
    }
}