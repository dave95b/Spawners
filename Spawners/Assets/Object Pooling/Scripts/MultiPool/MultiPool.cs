using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class MultiPool<T> : IPool<T>
    {
        private readonly IPool<T>[] pools;
        private readonly IMultiPoolSelector selector;

        private readonly PoolListener<T>[] listeners;

        internal MultiPool(IPool<T>[] pools, IMultiPoolSelector selector) : this(pools, selector, Array.Empty<PoolListener<T>>())
        {

        }

        internal MultiPool(IPool<T>[] pools, IMultiPoolSelector selector, PoolListener<T>[] listeners)
        {
            this.pools = pools;
            this.selector = selector;
            this.listeners = listeners;
        }


        public Poolable<T> Retrieve()
        {
            int index = selector.SelectPoolIndex();
            return RetrieveFrom(index);
        }

        public Poolable<T> RetrieveFrom(int poolIndex)
        {
            Assert.IsTrue(poolIndex < pools.Length);
            var poolable = pools[poolIndex].Retrieve();

            foreach (var listener in listeners)
                listener.OnRetrieved(poolable);

            return poolable;
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

            foreach (var listener in listeners)
            {
                foreach (var poolable in poolables)
                    listener.OnRetrieved(poolable);
            }
        }

        public void Return(Poolable<T> poolable)
        {
            foreach (var listener in listeners)
                listener.OnReturned(poolable);

            poolable.Pool.Return(poolable);
        }
    }
}