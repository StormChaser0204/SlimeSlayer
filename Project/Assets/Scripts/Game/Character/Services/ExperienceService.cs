using Game.Character.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class ExperienceService
    {
        private readonly SignalBus _signalBus;
        private readonly int _levelScaler;

        private int _currentLevel;
        private int _currentExpAmount;
        private int _nextLevelExpAmount;

        public ExperienceService(SignalBus signalBus, int expNextLevelExpAmount, int levelScaler)
        {
            _signalBus = signalBus;
            _levelScaler = levelScaler;
            _nextLevelExpAmount = expNextLevelExpAmount;
        }

        public void AddExperience(EnemyDiedSignal signal)
        {
            _currentExpAmount += 1;
            _signalBus.Fire(new ExperienceChangedSignal(_currentExpAmount));

            if (_currentExpAmount < _nextLevelExpAmount)
                return;

            _currentLevel++;
            _signalBus.Fire(new LevelUpSignal(_nextLevelExpAmount, _nextLevelExpAmount + _levelScaler,
                _currentLevel));
            _nextLevelExpAmount += _levelScaler;
        }
    }
}