using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    internal class PoolHelper<T>
    {
        private readonly PoolData<T> data;

        public PoolHelper(PoolData<T> data)
        {
            this.data = data;
        }


        public Poolable<T> Retrieve()
        {
            var pooledObjects = data.PooledObjects;
            int index = pooledObjects.Count - 1;
            var poolable = pooledObjects[index];
            pooledObjects.RemoveAt(index);

            data.UsedObjects.Add(poolable);

            poolable.gameObject.SetActive(true);

            return poolable;
        }

        public void Return(Poolable<T> poolable)
        {
            int index = data.UsedObjects.IndexOf(poolable);
            Assert.AreNotEqual(-1, index);

            data.UsedObjects.RemoveAtSwapBack(index);

            data.PooledObjects.Add(poolable);
            poolable.gameObject.SetActive(false);
        }
    }
}
