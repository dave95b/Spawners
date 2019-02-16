using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class MultiPool<T> : IPool<T> where T : Component
    {
        private readonly IPool<T>[] pools;
        private readonly MultiPoolSelector<T> selector;

        internal MultiPool(IPool<T>[] pools, MultiPoolSelector<T> selector)
        {
            this.pools = pools;
            this.selector = selector;
        }


        public Poolable<T> Retrieve()
        {
            var pool = selector.SelectPool();
            return pool.Retrieve();
        }

        public Poolable<T> RetrieveFrom(int poolIndex)
        {
            Assert.IsTrue(poolIndex < pools.Length);
            return pools[poolIndex].Retrieve();
        }

        public void RetrieveMany(Poolable<T>[] poolables)
        {
            RetrieveMany(poolables, poolables.Length);
        }

        public void RetrieveMany(Poolable<T>[] poolables, int count)
        {
            Assert.IsNotNull(poolables);
            Assert.IsTrue(count > 0);
            Assert.IsTrue(count <= poolables.Length);

            for (int i = 0; i < count; i++)
            {
                var poolable = Retrieve();
                poolables[i] = poolable;
            }
        }

        public void RetrieveManyFrom(Poolable<T>[] poolables, int poolIndex)
        {
            RetrieveManyFrom(poolables, poolIndex, poolables.Length);
        }

        public void RetrieveManyFrom(Poolable<T>[] poolables, int poolIndex, int count)
        {
            Assert.IsNotNull(poolables);
            Assert.IsTrue(count > 0);
            Assert.IsTrue(count <= poolables.Length);
            Assert.IsTrue(poolIndex < pools.Length);

            var pool = pools[poolIndex];
            pool.RetrieveMany(poolables, count);
        }

        public void ReturnAll()
        {
            foreach (var pool in pools)
                pool.ReturnAll();
        }
    }
}