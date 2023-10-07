using System;
using JetBrains.Annotations;
using Zenject;

namespace Game.Enemies.Data
{
    [UsedImplicitly]
    internal class Model : IPoolable<IMemoryPool>, IDisposable
    {
        public readonly View View;

        public Type Type;
        public float Damage;
        public float MaxHealth;
        public float Health;
        public float Speed;
        public bool EndPointReached;

        private IMemoryPool _pool;

        public Model(View view) => View = view;

        public void Init(Type type, float damage, int maxHealth, float speed)
        {
            Type = type;
            Damage = damage;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Speed = speed;
            EndPointReached = false;
        }

        public void OnDespawned() => _pool = null;

        public void OnSpawned(IMemoryPool pool) => _pool = pool;

        public void Dispose() => _pool.Despawn(this);

        public void SetViewActiveState(bool isActive) => View.gameObject.SetActive(isActive);
    }
}