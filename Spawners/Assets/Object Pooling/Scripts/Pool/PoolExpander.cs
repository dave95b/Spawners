﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectPooling
{
    internal class PoolExpander<T>
    {
        public Pool<T> Pool;
        private readonly List<Poolable<T>> pooledObjects;
        private readonly int expandAmount, instantiatedPerFrame;
        private readonly MonoBehaviour poolBehaviour;
        private readonly Poolable<T> prefab;

        public PoolExpander(List<Poolable<T>> pooledObjects, int expandAmount, int instantiatedPerFrame, MonoBehaviour poolBehaviour, Poolable<T> prefab)
        {
            this.pooledObjects = pooledObjects;
            this.expandAmount = expandAmount;
            this.instantiatedPerFrame = instantiatedPerFrame;
            this.poolBehaviour = poolBehaviour;
            this.prefab = prefab;
        }


        public void Expand()
        {
            Instantiate(expandAmount);
        }

        public void Instantiate(int amount)
        {
            if (amount <= instantiatedPerFrame)
                DoInstantiate(amount);
            else
                poolBehaviour.StartCoroutine(InstantiateCoroutine(amount));
        }

        private void DoInstantiate(int amount)
        {
            Transform parent = poolBehaviour.transform;
            for (int i = 0; i < amount; i++)
            {
                var created = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
                created.Pool = Pool;
                created.gameObject.SetActive(false);
                pooledObjects.Add(created);
            }
        }

        private IEnumerator InstantiateCoroutine(int amount)
        {
            while (amount > 0)
            {
                int toInstantiate = Mathf.Min(instantiatedPerFrame, amount);
                amount -= toInstantiate;
                DoInstantiate(toInstantiate);

                yield return null;
            }
        }
    }
}