using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class MultiPool<T> : IPool<T>
    {
        private readonly IPool<T>[] pools;
        private readonly IMultiPoolSelector selector;
        private readonly IPoolableStateResotrer<T> stateResotrer;


        internal MultiPool(IPool<T>[] pools, IMultiPoolSelector selector, IPoolableStateResotrer<T> stateResotrer)
        {
            Assert.IsNotNull(pools);
            Assert.IsNotNull(selector);

            this.pools = pools;
            this.selector = selector;
            this.stateResotrer = stateResotrer;
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
            stateResotrer?.Restore(poolable.Target);

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

            if (stateResotrer is null)
                return;

            for (int i = 0; i < count; i++)
                stateResotrer.Restore(poolables[i].Target);
        }

        public void Return(Poolable<T> poolable)
        {
            poolable.Pool.Return(poolable);
        }
    }
}