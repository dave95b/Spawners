using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class Pool 
    {
        private readonly PoolData data;
        private readonly PoolHelper helper;
        private readonly PoolExpander expander;

        internal Pool(PoolData data, PoolHelper helper, PoolExpander expander)
        {
            this.data = data;
            this.helper = helper;
            this.expander = expander;
        }


        public T Retrieve<T>() where T : Poolable
        {
            if (data.PooledObjects.Count == 0)
                expander.Expand();
            var retrieved = helper.Retrieve();

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(data.Size, data.PooledObjects.Count + data.UsedObjects.Count);

            return retrieved as T;
        }

        public void Return(Poolable poolable)
        {
            Assert.IsNotNull(poolable);

            helper.Return(poolable);

            Assert.AreEqual(data.Size, data.PooledObjects.Count + data.UsedObjects.Count);
        }

        public void ReturnAll()
        {
            helper.ReturnAll();

            Assert.AreEqual(data.Size, data.PooledObjects.Count);
        }
    }
}
