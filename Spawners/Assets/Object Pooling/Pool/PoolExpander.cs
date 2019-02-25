using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace SpawnerSystem.ObjectPooling
{
    public class PoolExpander<T>
    {
        public Pool<T> Pool;
        private readonly List<Poolable<T>> pooledObjects;
        private readonly int expandAmount, instantiatedPerFrame;
        private readonly Transform objectsParent;
        private readonly Poolable<T> prefab;

        private int instantiateAmount = 0;

        public PoolExpander(List<Poolable<T>> pooledObjects, int expandAmount, int instantiatedPerFrame, Transform objectsParent, Poolable<T> prefab)
        {
            Assert.IsNotNull(pooledObjects);
            Assert.IsTrue(expandAmount > 0);
            Assert.IsTrue(instantiatedPerFrame > 0);
            Assert.IsNotNull(objectsParent);
            Assert.IsNotNull(prefab);

            this.pooledObjects = pooledObjects;
            this.expandAmount = expandAmount;
            this.instantiatedPerFrame = instantiatedPerFrame;
            this.objectsParent = objectsParent;
            this.prefab = prefab;
        }


        public void Expand()
        {
            Expand(expandAmount);
        }

        public void Expand(int amount)
        {
            Assert.IsTrue(amount > 0);

            if (amount <= instantiatedPerFrame)
                Instantiate(amount);
            else
            {
                Instantiate(instantiatedPerFrame);
                instantiateAmount = amount - instantiatedPerFrame;
            }
        }

        public void Update()
        {
            if (instantiateAmount == 0)
                return;

            int toInstantiate = Mathf.Min(instantiatedPerFrame, instantiateAmount);
            instantiateAmount -= toInstantiate;
            Instantiate(toInstantiate);
        }

        private void Instantiate(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var created = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, objectsParent);
                created.Pool = Pool;
                created.gameObject.SetActive(false);
                pooledObjects.Add(created);
            }
        }
    }
}