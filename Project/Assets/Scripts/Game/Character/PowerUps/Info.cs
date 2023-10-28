namespace Game.Character.PowerUps
{
    [System.Serializable]
    internal class Info
    {
        public Type Type;
        public Type[] Required;
        public Type[] Blocked;
        public Modifiers.Type ModifierType;
        public int ModifierValue;
    }
}