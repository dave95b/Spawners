using ObjectManagement.ObjectPooling.Factory;
using ObjectManagement.ObjectPooling.StateRestorer;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ObjectManagement.ObjectPooling
{
    public class Pool<T> : IPool<T>
    {
        public IEnumerable<T> UsedObjects => usedObjects;
        public IStateRestorer<T> StateRestorer { get; set; }

        private readonly IPooledFactory<T> factory;
        private readonly int expandAmount;

        private readonly List<T> pooledObjects;
        private readonly HashSet<T> usedObjects;

        internal Pool(IStateRestorer<T> stateRestorer, IPooledFactory<T> factory, int expandAmount)
            : this(stateRestorer, factory, expandAmount, new List<T>(expandAmount))
        { }


        internal Pool(IStateRestorer<T> stateRestorer, IPooledFactory<T> factory, int expandAmount, List<T> pooledObjects)
        {
            Assert.IsNotNull(stateRestorer);
            Assert.IsNotNull(factory);
            Assert.IsNotNull(pooledObjects);

            StateRestorer = stateRestorer;
            this.factory = factory;
            this.expandAmount = expandAmount;

            this.pooledObjects = pooledObjects;
            usedObjects = new HashSet<T>();
        }


        public T Retrieve()
        {
            if (pooledObjects.Count == 0)
                Expand();

            T pooled = DoRetrieve();
            StateRestorer.OnRetrieve(pooled);

            return pooled;
        }

        private T DoRetrieve()
        {
            int index = pooledObjects.Count - 1;
            T pooled = pooledObjects[index];
            pooledObjects.RemoveAt(index);
            usedObjects.Add(pooled);

            return pooled;
        }


        public void Return(T pooled)
        {
            Assert.IsTrue(usedObjects.Contains(pooled));

            DoReturn(pooled);
            usedObjects.Remove(pooled);
        }

        public void ReturnAll()
        {
            foreach (var pooled in usedObjects)
                DoReturn(pooled);

            usedObjects.Clear();
        }

        private void DoReturn(T pooled)
        {
            pooledObjects.Add(pooled);
            StateRestorer.OnReturn(pooled);
        }


        private void Expand()
        {
            for (int i = 0; i < expandAmount; i++)
            {
                var created = factory.Create();
                pooledObjects.Add(created);
            }
        }
    }
}