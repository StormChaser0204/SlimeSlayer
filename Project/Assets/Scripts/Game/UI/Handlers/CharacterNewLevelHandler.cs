using Common.SignalHandler.Handlers;
using Game.Character.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class CharacterNewLevelHandler : SignalHandler
    {
        [Inject] private CharacterPanel _characterPanel;

        private readonly int _currentLevelExpAmount;
        private readonly int _nextLevelExpAmount;
        private readonly int _currentLevel;

        public CharacterNewLevelHandler(LevelUpSignal signal) : base(signal)
        {
            _currentLevelExpAmount = signal.CurrentLevelExpAmount;
            _nextLevelExpAmount = signal.NextLevelExpAmount;
            _currentLevel = signal.CurrentLevel;
        }

        public override void Handle() =>
            _characterPanel.SetNewLevelProgress(_currentLevelExpAmount, _nextLevelExpAmount,
                _currentLevel);
    }
}