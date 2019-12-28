using Experimental.ObjectPooling.StateRestorer;
using SpawnerSystem.Shared;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Experimental.ObjectPooling
{
    public class MultiPool<T> : IMultiPool<T>
    {
        public IEnumerable<T> UsedObjects => usedObjects.Keys;
        public IEnumerable<IPool<T>> Pools => pools;
        public ISelector Selector { get; set; }

        private readonly IPool<T>[] pools;
        private readonly IStateRestorer<T> stateRestorer;

        private readonly Dictionary<T, IPool<T>> usedObjects;


        public MultiPool(IPool<T>[] pools, ISelector selector) : this(pools, selector, null)
        { }
        
        public MultiPool(IPool<T>[] pools, ISelector selector, IStateRestorer<T> stateRestorer)
        {
            Assert.IsNotNull(pools);
            Assert.IsNotNull(selector);

            this.pools = pools;
            Selector = selector;
            this.stateRestorer = stateRestorer;

            usedObjects = new Dictionary<T, IPool<T>>();
        }


        public T Retrieve()
        {
            int index = Selector.SelectIndex();
            return RetrieveFrom(index);
        }

        public T RetrieveFrom(int index)
        {
            Assert.IsTrue(index >= 0);
            Assert.IsTrue(index < pools.Length);

            var pool = pools[index];
            
            return RetrieveFrom(pool);
        }

        public T RetrieveFrom(IPool<T> pool)
        {
            Assert.IsNotNull(pool);
            Assert.IsTrue(pools.Contains(pool));

            T retrieved = pool.Retrieve();
            stateRestorer?.OnRetrieve(retrieved);
            usedObjects.Add(retrieved, pool);

            return retrieved;
        }

        public void Return(T pooled)
        {
            Assert.IsTrue(usedObjects.ContainsKey(pooled));

            stateRestorer?.OnReturn(pooled);

            var pool = usedObjects[pooled];
            usedObjects.Remove(pooled);

            pool.Return(pooled);
        }

        public void ReturnAll()
        {
            if (stateRestorer != null)
            {
                foreach (var used in UsedObjects)
                    stateRestorer.OnReturn(used);
            }

            foreach (var pool in pools)
                pool.ReturnAll();

            usedObjects.Clear();
        }
    }
}
