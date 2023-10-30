using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class HealthModifier : ModifierBase
    {
        public HealthModifier(StatsInfo statsInfo, int value) : base(statsInfo, value)
        {
        }

        public override void Process() => StatsInfo.UpdateHealth(Value);
    }
}