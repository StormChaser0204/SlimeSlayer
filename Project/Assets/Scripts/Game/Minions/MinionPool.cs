using System.Linq;
using Game.Minions.Data;
using Game.Shared.Damage.Components;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Minions
{
    internal class MinionPool
    {
        private readonly SpawnedUnits _spawnedUnits;
        private readonly ObjectPool<MinionModel> _pool;
        private readonly MinionView _prefab;
        private readonly Vector3 _spawnPoint;

        public MinionPool(MinionView prefab, Transform spawnPoint, SpawnedUnits spawnedUnits)
        {
            _spawnedUnits = spawnedUnits;
            _prefab = prefab;
            _spawnPoint = spawnPoint.position;
            _pool = new ObjectPool<MinionModel>(CreateItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, 50, 100);
        }

        public void GetItem()
        {
            var inst = _pool.Get();
            _spawnedUnits.Add(inst);
        }

        private void ReturnToPool(IDamageTaker damageTaker)
        {
            var model = _spawnedUnits.First(i => i.DamageTaker == damageTaker);
            ReturnItem(model);
        }

        private void ReturnItem(MinionModel model)
        {
            _pool.Release(model);
            _spawnedUnits.Remove(model);
        }

        private MinionModel CreateItem()
        {
            var view = Object.Instantiate(_prefab);
            var damageTaker = new DamageTaker();
            var model = new MinionModel(view, 1, damageTaker, 1);
            view.CurrentPosition = _spawnPoint;
            damageTaker.OnDeath += ReturnToPool;
            return model;
        }

        private void OnTakeFromPool(MinionModel model)
        {
            model.View.gameObject.SetActive(true);
            model.View.transform.position = _spawnPoint;
            model.DamageTaker.Health = 1;
        }

        private static void OnReturnedToPool(MinionModel model) =>
            model.View.gameObject.SetActive(false);

        private static void OnDestroyPoolObject(MinionModel model) =>
            Object.Destroy(model.View.gameObject);
    }
}