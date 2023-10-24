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
        public int Damage;
        public float MaxHealth;
        public float Health;
        public float Speed;
        public float AttackCooldown;
        public bool EndPointReached;
        public bool IsAlive => Health != 0;

        private IMemoryPool _pool;
        private float _baseAttackCooldown;

        public Model(View view) => View = view;

        public void Init(Type type, int damage, int maxHealth, float speed, float attackCooldown)
        {
            Type = type;
            Damage = damage;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Speed = speed;
            AttackCooldown = attackCooldown;
            _baseAttackCooldown = attackCooldown;
            EndPointReached = false;
        }

        public void ResetAttackCooldown() => AttackCooldown = _baseAttackCooldown;

        public void OnDespawned() => _pool = null;

        public void OnSpawned(IMemoryPool pool) => _pool = pool;

        public void Dispose() => _pool.Despawn(this);

        public void SetViewActiveState(bool isActive) => View.gameObject.SetActive(isActive);
    }
}