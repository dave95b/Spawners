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

        [SerializeField, HideInInspector]
        private PoolData poolData;
        [SerializeField, HideInInspector]
        private PoolHelper helper;
        [SerializeField, HideInInspector]
        private PoolExpander expander;



        private void Reset()
        {
            var usedObjects = new List<Poolable>(size);
            var pooledObjects = new List<Poolable>(size);
            poolData = new PoolData(usedObjects, pooledObjects);
            helper = new PoolHelper(poolData);
            expander = new PoolExpander(poolData, expandAmount, instantiatedPerFrame, coroutineHolder: this);
            Pool = new Pool(poolData, helper, expander);
        }

        [Button]
        private void CreateObjects()
        {
            expander.Instantiate(size);
            //objectsInUse = new List<Poolable>(size);
            //    pooledObjects = new List<Poolable>(size);

            //    for (int i = 0; i < size; i++)
            //    {
            //        var created = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            //        created.Pool = this;
            //        created.gameObject.SetActive(false);
            //        pooledObjects.Add(created);
            //    }
        }

        [Button]
        private void ResetPool()
        {
            
        }
    }
}
