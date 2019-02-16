using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class Pool<T> : IPool<T> where T : Component
    {
        private readonly PoolData<T> data;
        private readonly PoolHelper<T> helper;
        private readonly PoolExpander<T> expander;

        private readonly PoolListener<T>[] listeners;

        internal Pool(PoolData<T> data, PoolHelper<T> helper, PoolExpander<T> expander, PoolListener<T>[] listeners)
        {
            this.data = data;
            this.helper = helper;
            this.expander = expander;
            this.listeners = listeners;
        }


        public Poolable<T> Retrieve()
        {
            if (data.PooledObjects.Count == 0)
                expander.Expand();

            var poolable = helper.Retrieve();
            Assert.IsNotNull(poolable);

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

            foreach (var listener in listeners)
                listener.OnReturned(poolable);

            helper.Return(poolable);
        }

        public void ReturnMany(Poolable<T>[] poolables)
        {
            ReturnMany(poolables, poolables.Length);
        }

        public void ReturnMany(Poolable<T>[] poolables, int count)
        {
            Assert.IsTrue(count <= poolables.Length);

            for (int i = 0; i < count; i++)
            {
                Return(poolables[i]);
                poolables[i] = null;
            }
        }

        public void ReturnAll()
        {
            foreach (var listener in listeners)
            {
                foreach (var poolable in data.UsedObjects)
                    listener.OnReturned(poolable);
            }
            helper.ReturnAll();
        }
    }
}
