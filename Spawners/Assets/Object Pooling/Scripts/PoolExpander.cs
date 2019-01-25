using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectPooling
{
    [Serializable]
    internal class PoolExpander
    {
        public Pool Pool;
        private PoolData data;
        private int expandAmount, instantiatedPerFrame;
        private MonoBehaviour poolBehaviour;
        private Poolable prefab;

        public PoolExpander(PoolData data, int expandAmount, int instantiatedPerFrame, MonoBehaviour poolBehaviour, Poolable prefab)
        {
            this.data = data;
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
                poolBehaviour.StartCoroutine(InstantiateInCoroutine(amount));
        }

        private void DoInstantiate(int amount)
        {
            Transform parent = poolBehaviour.transform;
            for (int i = 0; i < amount; i++)
            {
                var created = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
                created.Pool = Pool;
                created.gameObject.SetActive(false);
                data.PooledObjects.Add(created);
            }
        }

        private IEnumerator InstantiateInCoroutine(int amount)
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