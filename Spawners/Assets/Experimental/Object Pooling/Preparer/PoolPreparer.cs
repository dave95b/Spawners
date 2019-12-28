using Experimental.ObjectPooling.Builder;
using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Experimental.ObjectPooling.Preparer
{
    public abstract partial class PoolPreparer<T> : BasePoolPreparer<T>, IPoolPreparer<T> where T : Component
    {
        [SerializeField, MinValue(1)]
        private int size = 10, expandAmount = 5;

        [SerializeField]
        private T prefab;

        private IPool<T> pool;
        public IPool<T> Pool => pool ?? (pool = CreatePool());

        protected virtual IStateRestorer<T> StateRestorer { get; }
        protected virtual IPooledFactory<T> Factory { get; }


        private IPool<T> CreatePool()
        {
            var pooled = GetPooledObjects();

            int toExpand = Mathf.Max(0, size - pooled.Count);
            var builder = new ComponentPoolBuilder<T>(transform, prefab);

            return builder.WithInitialItems(toExpand)
                .WithExpandAmount(expandAmount)
                .WithFactory(Factory)
                .WithStateRestorer(StateRestorer)
                .Build(pooled);
        }

        private List<T> GetPooledObjects()
        {
            int count = transform.childCount;
            var pooledObjects = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                var pooled = transform.GetChild(i).GetComponent<T>();
                if (pooled != null)
                    pooledObjects.Add(pooled);
            }

            return pooledObjects;
        }
    }

#if UNITY_EDITOR

    public partial class PoolPreparer<T>
    {
        [Button]
        internal override void CreateObjects()
        {
            var stateRestorer = new DefaultComponentStateRestorer<T>(transform, StateRestorer);
            for (int i = 0; i < size; i++)
            {
                var created = UnityEditor.PrefabUtility.InstantiatePrefab(prefab, transform) as T;
                stateRestorer.OnReturn(created);
            }
        }
    }

#endif
}