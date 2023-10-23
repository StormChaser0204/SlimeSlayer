using Common.SignalDispatching.Binding;
using Common.SignalHandler;

namespace Common.SignalDispatching.Dispatcher
{
    public interface ISignalDispatcher
    {
        Binder<SignalHandlerBase> Bind();
        void Raise(ISignal ev);
    }
}