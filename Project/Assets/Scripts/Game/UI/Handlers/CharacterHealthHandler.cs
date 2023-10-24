using Common.SignalHandler.Handlers;
using Game.Character.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.UI.Handlers
{
    [UsedImplicitly]
    internal class CharacterHealthHandler : SignalHandler
    {
        [Inject] private CharacterPanel _characterPanel;

        private readonly int _totalHealth;
        private readonly int _newHealth;

        public CharacterHealthHandler(CharacterHealthChangedSignal signal) : base(signal)
        {
            _totalHealth = signal.TotalHealth;
            _newHealth = signal.NewHealth;
        }

        public override void Handle() => _characterPanel.UpdateHealth(_newHealth, _totalHealth);
    }
}