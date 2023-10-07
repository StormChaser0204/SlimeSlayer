namespace Game.Shared.Damage.Components
{
    internal interface IDamageTaker
    {
        public int MaxHealth { get; set; }
        public int Health { get; set; }
    }
}