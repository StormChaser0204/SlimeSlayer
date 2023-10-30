using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class HealthRegenerationModifier : ModifierBase
    {
        public HealthRegenerationModifier(StatsInfo statsInfo, int value) : base(statsInfo, value)
        {
        }

        public override void Process() => StatsInfo.UpdateDamage(Value);
    }
}