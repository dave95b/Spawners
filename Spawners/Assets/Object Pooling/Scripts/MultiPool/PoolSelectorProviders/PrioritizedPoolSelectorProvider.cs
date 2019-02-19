using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using NaughtyAttributes;
using System.Diagnostics;

namespace ObjectPooling
{
    internal class PrioritizedPoolSelectorProvider : PoolSelectorProvider
    {
        [SerializeField]
        private PrioritizedPoolSelector selector;

        [SerializeField]
        private List<Entry> priorities;

        public override IMultiPoolSelector PoolSelector
        {
            get
            {
                if (selector is null)
                {
                    int[] priorities = GetPriorities();
                    selector = new PrioritizedPoolSelector(priorities);
                }

                return selector;
            }
        }

        private int[] GetPriorities()
        {
            int length = priorities.Count;
            int[] result = new int[length];

            for (int i = 0; i < length; i++)
                result[i] = priorities[i].Priority;

            return result;
        }

        [Conditional("UNITY_EDITOR"), Button]
        protected void UpdatePriorities()
        {
            foreach (var provider in preparer.Providers)
            {
                if (!priorities.Any(p => p.PoolProvider == provider))
                    priorities.Add(new Entry(provider));
            }
            
            priorities.RemoveAll(entry => !preparer.Providers.Any(provider => entry.PoolProvider == provider));
        }

        [Serializable]
        private struct Entry
        {
            public PoolProvider PoolProvider;
            public int Priority;

            public Entry(PoolProvider provider)
            {
                PoolProvider = provider;
                Priority = 0;
            }
        }
    }
}