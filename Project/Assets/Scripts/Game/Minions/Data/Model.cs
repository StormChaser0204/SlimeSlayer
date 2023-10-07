using Game.Shared.Damage.Components;

namespace Game.Minions.Data
{
    internal class Model
    {
        public View View;
        public Type Type;
        public float Damage;
        public IDamageTaker DamageTaker;
        public float Speed;
        public bool EndPointReached;

        public Model(View view, Type type, float damage, IDamageTaker damageTaker, float speed)
        {
            View = view;
            Type = type;
            Damage = damage;
            DamageTaker = damageTaker;
            Speed = speed;
        }
    }
}