using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal class HealthRegenerationModifier : ModifierBase
    {
        public HealthRegenerationModifier(CharacterInfo characterInfo, int value) : base(characterInfo, value)
        {
        }

        public override void Process() => CharacterInfo.UpdateDamage(Value);
    }
}