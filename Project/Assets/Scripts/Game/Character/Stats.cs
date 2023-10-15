namespace Game.Character
{
    public class Stats
    {
        public float Damage { get; private set; }

        public Stats(float baseDamage) => Damage = baseDamage;

        public void UpdateDamage(float additionalDamage) => Damage += additionalDamage;
    }
}