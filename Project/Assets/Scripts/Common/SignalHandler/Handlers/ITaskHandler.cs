using System.Threading;
using Cysharp.Threading.Tasks;

namespace Common.SignalHandler.Handlers
{
    public interface ITaskHandler
    {
        UniTask Handle(CancellationToken cancellationToken = default);
    }
}