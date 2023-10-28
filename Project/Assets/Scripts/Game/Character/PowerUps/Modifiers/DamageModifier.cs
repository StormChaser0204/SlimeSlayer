using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class DamageModifier : ModifierBase
    {
        public DamageModifier(CharacterInfo characterInfo, int value) : base(characterInfo, value)
        {
        }

        public override void Process() => CharacterInfo.UpdateDamage(Value);
    }
}