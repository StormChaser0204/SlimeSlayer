using Common.SignalHandler;
using JetBrains.Annotations;

namespace Game.Character.Signals
{
    [UsedImplicitly]
    internal class CharacterHealthChangedSignal : ISignal
    {
        public readonly int TotalHealth;
        public readonly int NewHealth;

        public CharacterHealthChangedSignal(int totalHealth, int newHealth)
        {
            TotalHealth = totalHealth;
            NewHealth = newHealth;
        }
    }
}