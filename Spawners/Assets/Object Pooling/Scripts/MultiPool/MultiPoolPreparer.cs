using UnityEngine;
using System.Collections;
using System;
using NaughtyAttributes;
using System.Diagnostics;

namespace ObjectPooling
{
    internal abstract class MultiPoolPreparer : MonoBehaviour
    {
        public abstract PoolProvider[] Providers { get; }
    }

    internal abstract class MultiPoolPreparer<T> : MultiPoolPreparer
    {
        [SerializeField]
        private PoolSelectorProvider selectorProvider;

        protected abstract PoolProvider<T>[] PoolProviders { get; }
        public override PoolProvider[] Providers => PoolProviders;

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
            int poolCount = PoolProviders.Length;
            IPool<T>[] pools = new IPool<T>[poolCount];

            for (int i = 0; i < poolCount; i++)
                pools[i] = PoolProviders[i].Pool;

            var selector = selectorProvider.PoolSelector;
            var multiPool = new MultiPool<T>(pools, selector);

            return multiPool;
        }

        [Conditional("UNITY_EDITOR"), Button]
        protected abstract void FindPoolProviders();
    }
}
