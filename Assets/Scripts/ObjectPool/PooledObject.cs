using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.ObjectPool
{
    public class PooledObject : MonoBehaviour
    {
        public ObjectPoolService Service { get; private set; }
        public GameObject Prefab { get; private set; }
        public IObjectPool<PooledObject> Pool { get; private set; }

        public void Initialize(ObjectPoolService service, GameObject prefab, IObjectPool<PooledObject> pool)
        {
            Service = service;
            Prefab = prefab;
            Pool = pool;
        }

        /// <summary>
        /// Возвращает объект в пул.
        /// </summary>
        public void ReturnToPool()
        {
            if (Service != null)
                Service.Despawn(this);
            else
                Destroy(gameObject);
        }
    }
}