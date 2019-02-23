using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using SpawnerSystem.Shared;

namespace SpawnerSystem.ObjectPooling
{
    internal class RandomPoolSelectorProvider : PoolSelectorProvider
    {
        [SerializeField, ReadOnly]
        private int poolCount;

        private RandomSelector selector;
        public override ISelector PoolSelector
        {
            get
            {
                if (selector is null)
                    selector = new RandomSelector(poolCount);

                return selector;
            }
        }

        public override void Initialize<T>(PoolPreparer<T>[] poolPreparers, MultiPoolPreparer<T>[] multiPoolPreparers)
        {
            poolCount = poolPreparers.Length + multiPoolPreparers.Length;
        }
    }
}