using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class Pool<T> : IPool<T>
    {
        private readonly PoolData<T> data;
        private readonly PoolHelper<T> helper;
        private readonly PoolExpander<T> expander;
        private readonly IPoolableStateResotrer<T> stateResotrer;
        

        internal Pool(PoolData<T> data, PoolHelper<T> helper, PoolExpander<T> expander, IPoolableStateResotrer<T> stateResotrer)
        {
            Assert.IsNotNull(data);
            Assert.IsNotNull(helper);
            Assert.IsNotNull(expander);

            this.data = data;
            this.helper = helper;
            this.expander = expander;
            this.stateResotrer = stateResotrer;
        }


        public Poolable<T> Retrieve()
        {
            if (data.PooledObjects.Count == 0)
                expander.Expand();

            var poolable = helper.Retrieve();
            Assert.IsNotNull(poolable);
            stateResotrer?.Restore(poolable);

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

        public void Return(Poolable<T> poolable)
        {
            Assert.IsNotNull(poolable);
            helper.Return(poolable);
        }
    }
}
