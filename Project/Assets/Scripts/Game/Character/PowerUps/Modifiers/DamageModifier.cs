using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class DamageModifier : ModifierBase
    {
        public DamageModifier(StatsInfo statsInfo, int value) : base(statsInfo, value)
        {
        }

        public override void Process() => StatsInfo.UpdateDamage(Value);
    }
}