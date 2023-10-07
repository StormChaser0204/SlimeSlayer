using System.Linq;
using Game.Minions.Data;
using Game.Shared.Damage.Components;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Minions
{
    //TODO: Remake this pool. Need to use something good instead of unity pool.
    internal class MinionPool
    {
        private readonly SpawnedUnits _spawnedUnits;
        private readonly ObjectPool<Model> _pool;
        private readonly View _prefab;
        private readonly Vector3 _spawnPoint;

        public MinionPool(View prefab, Transform spawnPoint, SpawnedUnits spawnedUnits)
        {
            _spawnedUnits = spawnedUnits;
            _prefab = prefab;
            _spawnPoint = spawnPoint.position;
            _pool = new ObjectPool<Model>(CreateItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, 50, 100);
        }

        public Model GetItem()
        {
            var inst = _pool.Get();
            _spawnedUnits.Add(inst);
            return inst;
        }

        public void ReturnToPool(IDamageTaker damageTaker)
        {
            var model = _spawnedUnits.First(i => i.DamageTaker == damageTaker);
            ReturnItem(model);
        }

        private void ReturnItem(Model model)
        {
            _pool.Release(model);
            _spawnedUnits.Remove(model);
        }

        private Model CreateItem()
        {
            var view = Object.Instantiate(_prefab);
            var damageTaker = new DamageTaker(3);
            var model = new Model(view, Type.Small, 1, damageTaker, 1);
            view.CurrentPosition = _spawnPoint;
            return model;
        }

        private void OnTakeFromPool(Model model)
        {
            model.View.gameObject.SetActive(true);
            model.View.transform.position = _spawnPoint;
            model.DamageTaker.Health = model.DamageTaker.MaxHealth;
        }

        private static void OnReturnedToPool(Model model) =>
            model.View.gameObject.SetActive(false);

        private static void OnDestroyPoolObject(Model model) =>
            Object.Destroy(model.View.gameObject);
    }
}