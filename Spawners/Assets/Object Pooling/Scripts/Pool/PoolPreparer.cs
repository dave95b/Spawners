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
        protected abstract ListenersRepository<T> ListenersRepository { get; }

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
            var pooledObjects = new List<Poolable<T>>(size);
            GetPrewarmedObjects(pooledObjects);

            int toInstantiate = size - pooledObjects.Count;
            if (toInstantiate > 0)
                CreateObjects(toInstantiate, pooledObjects);

            var usedObjects = new List<Poolable<T>>(size);
            var poolData = new PoolData<T>(usedObjects, pooledObjects);
            var helper = new PoolHelper<T>(poolData);
            var expander = new PoolExpander<T>(poolData, expandAmount, instantiatedPerFrame, poolBehaviour: this, Prefab);
            Pool<T> pool = null;

            if (ListenersRepository != null)
            {
                var listeners = ListenersRepository.Listeners;
                pool = new Pool<T>(poolData, helper, expander, listeners);
            }
            else
                pool = new Pool<T>(poolData, helper, expander);

            expander.Pool = pool;

            foreach (var pooled in pooledObjects)
                pooled.Pool = pool;

            return pool;
        }


        [Conditional("UNITY_EDITOR"), Button]
        protected void CreateObjects()
        {
            for (int i = 0; i < size; i++)
            {
                var created = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
                created.gameObject.SetActive(false);
            }
        }

        private void CreateObjects(int amount, List<Poolable<T>> pooledObjects)
        {
            for (int i = 0; i < amount; i++)
            {
                var created = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
                created.gameObject.SetActive(false);
                pooledObjects.Add(created);
            }
        }

        private void GetPrewarmedObjects(List<Poolable<T>> pooledObjects)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var poolable = transform.GetChild(i).GetComponent<Poolable<T>>();
                if (poolable != null)
                    pooledObjects.Add(poolable);
            }
        }
    }
}
