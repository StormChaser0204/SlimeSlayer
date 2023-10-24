using System.Threading;
using Cysharp.Threading.Tasks;

namespace Common.SignalHandler.Handlers
{
    public abstract class TaskSignalHandler : SignalHandlerBase, ITaskHandler
    {
        protected TaskSignalHandler(ISignal signal) : base(signal)
        {
        }

        public abstract UniTask Handle(CancellationToken cancellationToken = default);
    }
}