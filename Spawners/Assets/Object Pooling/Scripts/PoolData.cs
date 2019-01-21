using UnityEngine;
using System;
using System.Collections.Generic;

namespace ObjectPooling
{
    [Serializable]
    internal class PoolData
    {
        public List<Poolable> UsedObjects, PooledObjects;

        public PoolData(List<Poolable> usedObjects, List<Poolable> pooledObjects)
        {
            UsedObjects = usedObjects;
            PooledObjects = pooledObjects;
        }
    }
}