using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace ObjectPooling
{
    internal abstract class PoolPreparer<T> : MonoBehaviour
    {
        [SerializeField, MinValue(1)]
        private int size = 10, expandAmount = 5, instantiatedPerFrame = 10;

        protected abstract Poolable<T> Prefab { get; }
        protected abstract List<Poolable<T>> PooledObjects { get; }
        protected abstract PoolListener<T>[] Listeners { get; }

        private Pool<T> pool;
        public Pool<T> Pool
        {
            get
            {
                if (pool is null)
                    pool = CreatePool();
                return pool;
            }
        }

        private Pool<T> CreatePool()
        {
            GetPrewarmedObjects();
            int toInstantiate = size - PooledObjects.Count;
            if (toInstantiate > 0)
                CreateObjects(toInstantiate, addToPooled: true);

            var usedObjects = new List<Poolable<T>>(size);
            var poolData = new PoolData<T>(usedObjects, PooledObjects);
            var helper = new PoolHelper<T>(poolData);
            var expander = new PoolExpander<T>(poolData, expandAmount, instantiatedPerFrame, poolBehaviour: this, Prefab);
            var pool = new Pool<T>(poolData, helper, expander, Listeners);
            expander.Pool = pool;

            foreach (var pooled in PooledObjects)
                pooled.Pool = pool;

            return pool;
        }


        [Conditional("UNITY_EDITOR"), Button]
        protected void CreateObjects()
        {
            CreateObjects(size, addToPooled: false);
        }

        private void CreateObjects(int amount, bool addToPooled)
        {
            for (int i = 0; i < amount; i++)
            {
                var created = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
                created.gameObject.SetActive(false);

                if (addToPooled)
                    PooledObjects.Add(created);
            }
        }

        private void GetPrewarmedObjects()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var poolable = transform.GetChild(i).GetComponent<Poolable<T>>();
                if (poolable != null)
                    PooledObjects.Add(poolable);
            }
        }
    }
}
