using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Collections;

namespace ObjectPooling
{
    [Serializable]
    internal class PoolHelper
    {
        private readonly PoolData data;

        public PoolHelper(PoolData data)
        {
            this.data = data;
        }


        public Poolable Retrieve()
        {
            var pooledObjects = data.PooledObjects;
            int index = pooledObjects.Count - 1;
            var poolable = pooledObjects[index];
            pooledObjects.RemoveAt(index);

            data.UsedObjects.Add(poolable);

            poolable.gameObject.SetActive(true);

            return poolable;
        }

        public void Return(Poolable poolable)
        {
            int index = data.UsedObjects.IndexOf(poolable);
            if (index == -1)
                return;

            data.UsedObjects.RemoveAtSwapBack(index);

            data.PooledObjects.Add(poolable);
            poolable.gameObject.SetActive(false);
        }

        public void ReturnAll()
        {
            var usedObjects = data.UsedObjects;
            var pooledObjects = data.PooledObjects;
            int count = data.UsedObjects.Count;

            for (int i = 0; i < count; i++)
            {
                var used = usedObjects[i];
                pooledObjects.Add(used);
                used.gameObject.SetActive(false);
            }

            usedObjects.Clear();
        }
    }
}
