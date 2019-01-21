using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    [System.Serializable]
    public class Pool
    {
        [SerializeField]
        private PoolData data;
        [SerializeField]
        private PoolHelper helper;
        [SerializeField]
        private PoolExpander expander;

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

            var retrieved = helper.Retrieve() as T;
            Assert.IsNotNull(retrieved);

            return retrieved;
        }

        public void Return(Poolable poolable)
        {
            Assert.IsNotNull(poolable);

            helper.Return(poolable);
        }

        public void ReturnAll()
        {
            helper.ReturnAll();
        }
    }
}
