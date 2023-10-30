using System.Threading;
using Common.Dialogs;
using Common.SignalDispatching.Dispatcher;
using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using Cysharp.Threading.Tasks;
using Game.Character.PowerUps;
using Game.Character.Services;
using Game.Common.GameState.Signals;
using Game.UI.Dialogs;
using Game.UI.Dialogs.PowerUp;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Handlers
{
    [UsedImplicitly]
    internal class PowerUpHandler : TaskSignalHandler
    {
        [Inject] private IDialogsLauncher _dialogsLauncher;
        [Inject] private ISignalDispatcher _dispatcher;
        [Inject] private PowerUpService _powerUpService;

        public PowerUpHandler(ISignal signal) : base(signal)
        {
        }

        public override async UniTask Handle(CancellationToken cancellationToken = default)
        {
            _dispatcher.Raise(new PauseGameSignal());
            var dialog = _dialogsLauncher.Show<PowerUpDialog>(DialogType.PowerUp);
            dialog.FillPowerUps(_powerUpService.PickRandom(3));
            Info selectedPowerUp = null;
            dialog.OnSelectionFinished = info => selectedPowerUp = info;

            await UniTask.WaitWhile(() => selectedPowerUp == null,
                cancellationToken: cancellationToken);
            _powerUpService.ProcessPowerUp(selectedPowerUp);
            _dispatcher.Raise(new ResumeGameSignal());
        }
    }
}