using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class AttackRangeModifier : ModifierBase
    {
        public AttackRangeModifier(CharacterInfo characterInfo, int value) : base(characterInfo, value)
        {
        }

        public override void Process() => CharacterInfo.UpdateDamage(Value);
    }
}