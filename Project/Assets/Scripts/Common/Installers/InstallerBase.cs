using Common.SignalDispatching.Dispatcher;
using Zenject;

namespace Common.Installers
{
    public abstract class InstallerBase
    {
        protected DiContainer Container;
        protected ISignalDispatcher Dispatcher;

        public void Install(DiContainer container, ISignalDispatcher dispatcher)
        {
            Container = container;
            Dispatcher = dispatcher;

            BindServices();
            BindHandlers();
        }

        protected abstract void BindServices();
        protected abstract void BindHandlers();

        public bool IsEnabled { get; }
    }
}