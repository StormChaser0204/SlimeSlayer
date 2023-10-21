using JetBrains.Annotations;

namespace Game.Enemies.Signals
{
    [UsedImplicitly]
    internal class CharacterHealthChangedSignal
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