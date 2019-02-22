using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    internal class PoolHelper<T>
    {
        private readonly List<Poolable<T>> pooledObjects;

        public PoolHelper(List<Poolable<T>> pooledObjects)
        {
            Assert.IsNotNull(pooledObjects);
            this.pooledObjects = pooledObjects;
        }


        public Poolable<T> Retrieve()
        {
            int index = pooledObjects.Count - 1;
            var poolable = pooledObjects[index];
            pooledObjects.RemoveAt(index);

            poolable.gameObject.SetActive(true);

            return poolable;
        }

        public void Return(Poolable<T> poolable)
        {
            pooledObjects.Add(poolable);
            poolable.gameObject.SetActive(false);
        }
    }
}
