using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal abstract class ModifierBase
    {
        protected readonly StatsInfo StatsInfo;
        protected readonly int Value;

        protected ModifierBase(StatsInfo statsInfo, int value)
        {
            StatsInfo = statsInfo;
            Value = value;
        }

        public abstract void Process();
    }
}