using UnityEngine;
using UnityEngine.Pool;

namespace Game.Location
{
    internal class EnvironmentPool
    {
        private readonly SpawnedElements _spawnedElements;
        private readonly ObjectPool<ElementView> _pool;
        private readonly ElementView _prefab;
        private readonly Vector3 _spawnPoint;
        private readonly Transform _parent;

        public EnvironmentPool(ElementView prefab, Transform spawnPoint, Transform parent,
            SpawnedElements spawnedElements)
        {
            _spawnedElements = spawnedElements;
            _prefab = prefab;
            _spawnPoint = spawnPoint.position;
            _parent = parent;
            _pool = new ObjectPool<ElementView>(CreateItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, 50, 100);
        }

        public ElementView GetItem()
        {
            var inst = _pool.Get();
            _spawnedElements.Add(inst);
            return inst;
        }

        public void ReturnToPool(ElementView view)
        {
            _pool.Release(view);
            _spawnedElements.Remove(view);
        }

        private ElementView CreateItem()
        {
            var view = Object.Instantiate(_prefab, _parent, true);
            view.transform.position = _spawnPoint;
            return view;
        }

        private void OnTakeFromPool(ElementView view)
        {
            view.gameObject.SetActive(true);
            view.transform.position = _spawnPoint;
        }

        private static void OnReturnedToPool(ElementView view) =>
            view.gameObject.SetActive(false);

        private static void OnDestroyPoolObject(ElementView view) =>
            Object.Destroy(view.gameObject);
    }
}