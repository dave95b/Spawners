using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    internal class RandomPoolSelectorProvider : PoolSelectorProvider
    {
        private RandomPoolSelector selector;
        public override IMultiPoolSelector PoolSelector
        {
            get
            {
                if (selector is null)
                {
                    var poolProviders = preparer.Providers;
                    selector = new RandomPoolSelector(poolProviders.Length);
                }

                return selector;
            }
        }
    }
}