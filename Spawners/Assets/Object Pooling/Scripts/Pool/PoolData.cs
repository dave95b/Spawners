using UnityEngine;
using System;
using System.Collections.Generic;

namespace ObjectPooling
{
    internal class PoolData<T>
    {
        public readonly List<Poolable<T>> UsedObjects, PooledObjects;

        public PoolData(List<Poolable<T>> usedObjects, List<Poolable<T>> pooledObjects)
        {
            UsedObjects = usedObjects;
            PooledObjects = pooledObjects;
        }
    }
}