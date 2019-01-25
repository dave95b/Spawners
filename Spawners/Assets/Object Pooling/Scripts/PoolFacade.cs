using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;

namespace ObjectPooling
{
    internal class PoolFacade : MonoBehaviour
    {
        [SerializeField, MinValue(1)]
        private int size = 10, expandAmount = 5, instantiatedPerFrame = 10;

        [SerializeField]
        private Poolable prefab;

        [HideInInspector]
        public Pool Pool;

        private PoolData poolData;
        private PoolHelper helper;
        private PoolExpander expander;

        [SerializeField, ReadOnly]
        private List<Poolable> pooledObjects = new List<Poolable>();


        private void Awake()
        {
            int toInstantiate = size - pooledObjects.Count;
            if (toInstantiate > 0)
                DoCreateObjects(toInstantiate);

            var usedObjects = new List<Poolable>(size);
            poolData = new PoolData(usedObjects, pooledObjects);
            helper = new PoolHelper(poolData);
            expander = new PoolExpander(poolData, expandAmount, instantiatedPerFrame, poolBehaviour: this, prefab);
            Pool = new Pool(poolData, helper, expander);
            expander.Pool = Pool;

            foreach (var pooled in pooledObjects)
                pooled.Pool = Pool;
        }


        [Button]
        private void CreateObjects()
        {
            DoCreateObjects(size);
        }

        private void DoCreateObjects(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var created = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
                created.gameObject.SetActive(false);
                pooledObjects.Add(created);
            }
        }

        [Button]
        private void ResetPool()
        {
            pooledObjects.Clear();
        }
    }
}
