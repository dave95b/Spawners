using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class Pool : MonoBehaviour
    {
        [SerializeField, MinValue(1)]
        private int size = 10;

        [SerializeField]
        private Poolable prefab;

        [SerializeField, NaughtyAttributes.ReadOnly]
        private List<Poolable> objectsInUse, pooledObjects;


        private void Awake()
        {
            if (pooledObjects == null || pooledObjects.Count != size)
                CreateObjects();
        }


        public T Retrieve<T>() where T : Poolable
        {
            int index = pooledObjects.Count - 1;
            var poolable = pooledObjects[index];
            pooledObjects.RemoveAt(index);

            objectsInUse.Add(poolable);

            poolable.gameObject.SetActive(true);
            T result = poolable as T;

            Assert.IsNotNull(result);
            Assert.AreEqual(size, pooledObjects.Count + objectsInUse.Count);

            return result;
        }

        public void Return(Poolable poolable)
        {
            Assert.IsNotNull(poolable);

            int index = objectsInUse.IndexOf(poolable);
            if (index == -1)
                return;
            objectsInUse.RemoveAtSwapBack(index);

            pooledObjects.Add(poolable);
            poolable.gameObject.SetActive(false);

            Assert.AreEqual(size, pooledObjects.Count + objectsInUse.Count);
        }

        public void ReturnAll()
        {
            foreach (var used in objectsInUse)
            {
                pooledObjects.Add(used);
                used.gameObject.SetActive(false);
            }

            objectsInUse.Clear();

            Assert.AreEqual(size, pooledObjects.Count);
        }

        public void Expand(int objectsToAdd)
        {

        }

        [Button]
        private void CreateObjects()
        {
            objectsInUse = new List<Poolable>(size);
            pooledObjects = new List<Poolable>(size);

            for (int i = 0; i < size; i++)
            {
                var created = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
                created.Pool = this;
                created.gameObject.SetActive(false);
                pooledObjects.Add(created);
            }
        }
    }
}
