using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Assets.Scripts.ObjectPool
{
    public class ObjectPoolService : IDisposable
    {
        private readonly Dictionary<GameObject, IObjectPool<PooledObject>> _pools = new();

        private const int DefaultCapacity = 10;
        private const int MaxSize = 100;

        /// <summary>
        /// Спавнит объект. Если пула для префаба нет — создаёт новый.
        /// </summary>
        public PooledObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            if (prefab == null)
            {
                Debug.LogError("[ObjectPoolService] Prefab is null");
                return null;
            }

            var pool = GetOrCreatePool(prefab);
            var pooled = pool.Get();

            var tr = pooled.transform;
            tr.SetPositionAndRotation(position, rotation);
            if (parent != null)
                tr.SetParent(parent, true);

            return pooled;
        }

        public void Despawn(PooledObject pooled)
        {
            if (pooled == null) return;

            if (pooled.Pool != null)
            {
                pooled.Pool.Release(pooled);
            }
            else
            {
                Object.Destroy(pooled.gameObject);
            }
        }

        // Удобный перегруз для тех, кто передаёт GameObject
        public void Despawn(GameObject instance)
        {
            if (instance == null) return;

            if (instance.TryGetComponent<PooledObject>(out var pooled))
                Despawn(pooled);
            else
                Object.Destroy(instance);
        }

        private IObjectPool<PooledObject> GetOrCreatePool(GameObject prefab)
        {
            if (_pools.TryGetValue(prefab, out var existing))
                return existing;

            var newPool = new ObjectPool<PooledObject>(
                createFunc: () => CreateInstance(prefab),
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroy,
                collectionCheck: true,
                defaultCapacity: DefaultCapacity,
                maxSize: MaxSize
            );

            _pools[prefab] = newPool;
            return newPool;
        }

        private PooledObject CreateInstance(GameObject prefab)
        {
            var instance = Object.Instantiate(prefab);
            instance.name = $"{prefab.name} (Pooled)";

            if (!instance.TryGetComponent<PooledObject>(out var pooled))
                pooled = instance.AddComponent<PooledObject>();

            // Передаём ссылку на пул, чтобы объект сам мог вернуться
            pooled.Initialize(this, prefab, _pools[prefab]);
            return pooled;
        }

        private void OnGet(PooledObject pooled)
        {
            pooled.gameObject.SetActive(true);
        }

        private void OnRelease(PooledObject pooled)
        {
            pooled.gameObject.SetActive(false);
            pooled.transform.SetParent(null);
        }

        private void OnDestroy(PooledObject pooled)
        {
            Object.Destroy(pooled.gameObject);
        }

        public void ClearAll()
        {
            foreach (var pool in _pools.Values)
                pool.Clear();

            _pools.Clear();
        }

        public void Dispose()
        {
            ClearAll();
        }
    }
}