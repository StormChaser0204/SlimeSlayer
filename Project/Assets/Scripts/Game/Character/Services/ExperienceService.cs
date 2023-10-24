using Common.SignalDispatching.Dispatcher;
using Game.Character.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class ExperienceService
    {
        [Inject] private ISignalDispatcher _dispatcher;

        private readonly int _levelScaler;

        private int _currentLevel;
        private int _currentExpAmount;
        private int _nextLevelExpAmount;

        public ExperienceService(int expNextLevelExpAmount, int levelScaler)
        {
            _levelScaler = levelScaler;
            _nextLevelExpAmount = expNextLevelExpAmount;
        }

        public void AddExperience(int amount)
        {
            _currentExpAmount += amount;
            _dispatcher.Raise(new ExperienceChangedSignal(_currentExpAmount));

            if (_currentExpAmount < _nextLevelExpAmount)
                return;

            _currentLevel++;
            _dispatcher.Raise(new LevelUpSignal(_nextLevelExpAmount, _nextLevelExpAmount + _levelScaler,
                _currentLevel));

            _nextLevelExpAmount += _levelScaler;
        }
    }
}