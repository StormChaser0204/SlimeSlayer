using Game.Character.Data;

namespace Game.Character.PowerUps.Modifiers
{
    internal abstract class ModifierBase
    {
        protected readonly CharacterInfo CharacterInfo;
        protected readonly int Value;

        protected ModifierBase(CharacterInfo characterInfo, int value)
        {
            CharacterInfo = characterInfo;
            Value = value;
        }

        public abstract void Process();
    }
}