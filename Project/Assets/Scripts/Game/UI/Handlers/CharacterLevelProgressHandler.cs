using Common.SignalHandler.Handlers;
using Game.Character.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class CharacterLevelProgressHandler : SignalHandler
    {
        [Inject] private CharacterPanel _characterPanel;

        private readonly int _currentExpAmount;

        public CharacterLevelProgressHandler(ExperienceChangedSignal signal) : base(signal) =>
            _currentExpAmount = signal.CurrentExpAmount;

        public override void Handle() => _characterPanel.UpdateLevelProgress(_currentExpAmount);
    }
}