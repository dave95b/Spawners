using Experimental.ObjectPooling.StateRestorer;
using SpawnerSystem.Shared;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Experimental.ObjectPooling
{
    public class MultiPool<T> : IMultiPool<T>
    {
        public IEnumerable<T> UsedObjects => usedObjects.Keys;
        public IReadOnlyList<IPool<T>> Pools => pools;
        public ISelector Selector { get; set; }
        public IStateRestorer<T> StateRestorer { get; set; }

        private readonly IPool<T>[] pools;
        private readonly Dictionary<T, IPool<T>> usedObjects;


        public MultiPool(IPool<T>[] pools, ISelector selector) : this(pools, selector, null)
        { }
        
        public MultiPool(IPool<T>[] pools, ISelector selector, IStateRestorer<T> stateRestorer)
        {
            Assert.IsNotNull(pools);
            Assert.IsTrue(pools.Length > 0);
            Assert.IsNotNull(selector);

            this.pools = pools;
            Selector = selector;
            StateRestorer = stateRestorer;

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
            StateRestorer?.OnRetrieve(retrieved);
            usedObjects.Add(retrieved, pool);

            return retrieved;
        }

        public void Return(T pooled)
        {
            Assert.IsTrue(usedObjects.ContainsKey(pooled));

            StateRestorer?.OnReturn(pooled);

            var pool = usedObjects[pooled];
            usedObjects.Remove(pooled);

            pool.Return(pooled);
        }

        public void ReturnAll()
        {
            if (StateRestorer != null)
            {
                foreach (var used in UsedObjects)
                    StateRestorer.OnReturn(used);
            }

            foreach (var pool in pools)
                pool.ReturnAll();

            usedObjects.Clear();
        }
    }
}
