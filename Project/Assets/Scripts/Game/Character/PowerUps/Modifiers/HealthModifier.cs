using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class HealthModifier : ModifierBase
    {
        public HealthModifier(CharacterInfo characterInfo, int value) : base(characterInfo, value)
        {
        }

        public override void Process() => CharacterInfo.UpdateHealth(Value);
    }
}