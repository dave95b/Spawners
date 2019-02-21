using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

namespace ObjectPooling
{
    internal class RandomPoolSelectorProvider : PoolSelectorProvider
    {
        [SerializeField, ReadOnly]
        private int poolCount;

        private RandomPoolSelector selector;
        public override IMultiPoolSelector PoolSelector
        {
            get
            {
                if (selector is null)
                    selector = new RandomPoolSelector(poolCount);

                return selector;
            }
        }

        public override void Initialize<T>(PoolPreparer<T>[] poolPreparers, MultiPoolPreparer<T>[] multiPoolPreparers)
        {
            poolCount = poolPreparers.Length + multiPoolPreparers.Length;
        }
    }
}