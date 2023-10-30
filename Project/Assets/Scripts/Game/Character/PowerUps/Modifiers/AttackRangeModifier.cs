using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class AttackRangeModifier : ModifierBase
    {
        public AttackRangeModifier(StatsInfo statsInfo, int value) : base(statsInfo, value)
        {
        }

        public override void Process() => StatsInfo.UpdateDamage(Value);
    }
}