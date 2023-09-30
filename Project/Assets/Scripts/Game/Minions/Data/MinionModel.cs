using Game.Shared.Damage.Components;

namespace Game.Minions.Data
{
    internal class MinionModel
    {
        public MinionView View;
        public float Damage;
        public IDamageTaker DamageTaker;
        public float Speed;
        public bool EndPointReached;

        public MinionModel(MinionView view, float damage, IDamageTaker damageTaker, float speed)
        {
            View = view;
            Damage = damage;
            DamageTaker = damageTaker;
            Speed = speed;
        }
    }
}