using UnityEngine;
using UnityEngine.Assertions;

namespace SpawnerSystem.ObjectPooling
{
    public interface IPoolableFactory<T>
    {
        Pool<T> Pool { get; set; }
        Poolable<T> Create();
    }

    public class PoolableFactory<T> : IPoolableFactory<T>
    {
        public Pool<T> Pool { get; set; }
        private readonly Poolable<T> prefab;
        private readonly Transform parent;

        public PoolableFactory(Poolable<T> prefab, Transform parent)
        {
            Assert.IsNotNull(prefab);
            Assert.IsNotNull(parent);

            this.prefab = prefab;
            this.parent = parent;
        }

        public Poolable<T> Create()
        {
            var created = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            created.Pool = Pool;
            return created;
        }
    }
}