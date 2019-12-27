using Experimental.ObjectPooling.Builder;
using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Experimental.ObjectPooling.Preparer
{
    public abstract partial class PoolPreparer<T> : MonoBehaviour where T : Component
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
            var pooled = new List<T>();
            GetPrewarmedObjects(pooled);

            int toExpand = Mathf.Max(0, size - pooled.Count);
            var builder = new ComponentPoolBuilder<T>(transform, prefab);

            return builder.WithInitialItems(toExpand)
                .WithExpandAmount(expandAmount)
                .WithFactory(Factory)
                .WithStateRestorer(StateRestorer)
                .Build(pooled);
        }

        private void GetPrewarmedObjects(List<T> pooledObjects)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var pooled = transform.GetChild(i).GetComponent<T>();
                if (pooled != null)
                    pooledObjects.Add(pooled);
            }
        }
    }

#if UNITY_EDITOR

    public partial class PoolPreparer<T>
    {
        [Button]
        protected void CreateObjects()
        {
            var stateRestorer = StateRestorer ?? new DefaultComponentStateRestorer<T>(transform);
            for (int i = 0; i < size; i++)
            {
                var created = UnityEditor.PrefabUtility.InstantiatePrefab(prefab, transform) as T;
                stateRestorer.OnReturn(created);
            }
        }
    }

#endif
}