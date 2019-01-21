using UnityEngine;
using System;
using System.Collections.Generic;

namespace ObjectPooling
{
    [Serializable]
    internal class PoolData
    {
        public int Size;
        public readonly List<Poolable> UsedObjects, PooledObjects;

        public PoolData(List<Poolable> usedObjects, List<Poolable> pooledObjects)
        {
            Size = 0;
            UsedObjects = usedObjects;
            PooledObjects = pooledObjects;
        }
    }
}