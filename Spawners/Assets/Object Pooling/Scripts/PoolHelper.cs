using UnityEngine;
using System;
using System.Collections.Generic;

namespace ObjectPooling
{
    [Serializable]
    internal class PoolHelper
    {
        private Pool pool;
        private List<Poolable> objectsInUse, pooledObjects;

        public PoolHelper(Pool pool, List<Poolable> objectsInUse, List<Poolable> pooledObjects)
        {
            this.pool = pool;
            this.objectsInUse = objectsInUse;
            this.pooledObjects = pooledObjects;
        }


        public Poolable Retrieve()
        {
            return null;
        }

        public void Return()
        {

        }

        public void ReturnAll()
        {

        }
    }
}
