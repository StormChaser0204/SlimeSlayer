using System.Threading;
using Common.SignalDispatching.Dispatcher;
using Common.SignalHandler.Handlers;
using Cysharp.Threading.Tasks;
using Game.Character.Signals;
using Game.Common.GameState.Signals;
using Game.UI.Dialogs;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class CharacterNewLevelHandler : SignalHandler
    {
        [Inject] private CharacterPanel _characterPanel;
        [Inject] private ISignalDispatcher _dispatcher;

        private readonly int _currentLevelExpAmount;
        private readonly int _nextLevelExpAmount;
        private readonly int _currentLevel;

        public CharacterNewLevelHandler(LevelUpSignal signal) : base(signal)
        {
            _currentLevelExpAmount = signal.CurrentLevelExpAmount;
            _nextLevelExpAmount = signal.NextLevelExpAmount;
            _currentLevel = signal.CurrentLevel;
        }

        public override void Handle()
        {
            _dispatcher.Raise(new SelectPowerUpSignal());
            _characterPanel.SetNewLevelProgress(_currentLevelExpAmount, _nextLevelExpAmount,
                _currentLevel);
        }
    }
}