using UnityEngine;
using System.Collections;
using System;
using NaughtyAttributes;
using System.Diagnostics;

namespace SpawnerSystem.ObjectPooling
{
    internal abstract class MultiPoolPreparer<T> : MonoBehaviour
    {
        [SerializeField]
        private PoolSelectorProvider selectorProvider;

        protected abstract PoolPreparer<T>[] PoolPreparers { get; }
        protected abstract MultiPoolPreparer<T>[] MultiPoolPreparers { get; }
        protected abstract IPoolableStateResotrer<T> StateRestorer { get; }

        private MultiPool<T> multiPool;
        public MultiPool<T> MultiPool
        {
            get
            {
                if (multiPool is null)
                    multiPool = CreateMultiPool();
                return multiPool;
            }
        }

        private MultiPool<T> CreateMultiPool()
        {
            var pools = GetPools();
            var selector = selectorProvider.PoolSelector;
            var multiPool = new MultiPool<T>(pools, selector, StateRestorer);

            return multiPool;
        }

        private IPool<T>[] GetPools()
        {
            int poolCount = PoolPreparers.Length;
            int multiPoolCount = MultiPoolPreparers.Length;
            var pools = new IPool<T>[poolCount + multiPoolCount];

            int i;
            for (i = 0; i < poolCount; i++)
                pools[i] = PoolPreparers[i].Pool;

            for (int j = 0; j < multiPoolCount; j++)
            {
                pools[i] = MultiPoolPreparers[j].MultiPool;
                i++;
            }

            return pools;
        }

        [Conditional("UNITY_EDITOR"), Button]
        protected abstract void FindPoolPreparers();

        [Conditional("UNITY_EDITOR"), Button]
        protected void InitializeSelector()
        {
            selectorProvider?.Initialize(PoolPreparers, MultiPoolPreparers);
        }

        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            InitializeSelector();
        }

        protected bool PreparersPredicate(PoolPreparer<T> preparer)
        {
            return preparer != this && preparer.transform.parent == transform;
        }

        protected bool PreparersPredicate(MultiPoolPreparer<T> preparer)
        {
            return preparer != this && preparer.transform.parent == transform;
        }
    }
}
