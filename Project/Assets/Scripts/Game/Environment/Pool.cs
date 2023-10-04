using UnityEngine;
using UnityEngine.Pool;

namespace Game.Environment
{
    internal class Pool
    {
        private readonly SpawnedElements _spawnedElements;
        private readonly ObjectPool<View> _pool;
        private readonly View _prefab;
        private readonly Vector3 _spawnPoint;
        private readonly Transform _parent;

        public Pool(View prefab, Transform spawnPoint, Transform parent,
            SpawnedElements spawnedElements)
        {
            _spawnedElements = spawnedElements;
            _prefab = prefab;
            _spawnPoint = spawnPoint.position;
            _parent = parent;
            _pool = new ObjectPool<View>(CreateItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, 50, 100);
        }

        public View GetItem()
        {
            var inst = _pool.Get();
            _spawnedElements.Add(inst);
            return inst;
        }

        public void ReturnToPool(View view)
        {
            _pool.Release(view);
            _spawnedElements.Remove(view);
        }

        private View CreateItem()
        {
            var view = Object.Instantiate(_prefab, _parent, true);
            view.transform.localPosition = new Vector2(_spawnPoint.x, 0);
            return view;
        }

        private void OnTakeFromPool(View view)
        {
            view.gameObject.SetActive(true);
            view.transform.position = new Vector2(_spawnPoint.x, 0);
        }

        private static void OnReturnedToPool(View view)
        {
            view.gameObject.SetActive(false);
            view.Dispose();
        }

        private static void OnDestroyPoolObject(View view) =>
            Object.Destroy(view.gameObject);
    }
}