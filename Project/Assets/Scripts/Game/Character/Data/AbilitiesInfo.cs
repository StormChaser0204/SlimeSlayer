using JetBrains.Annotations;

namespace Game.Character.Data
{
    [UsedImplicitly]
    internal class AbilitiesInfo
    {
        public float AttackCooldown;
        public float BlockCooldown;

        public AbilitiesInfo(float attackCooldown, float blockCooldown)
        {
            AttackCooldown = attackCooldown;
            BlockCooldown = blockCooldown;
        }
    }
}