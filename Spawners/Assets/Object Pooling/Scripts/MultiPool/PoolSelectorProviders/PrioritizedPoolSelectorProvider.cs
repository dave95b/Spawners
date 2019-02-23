using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using NaughtyAttributes;
using System.Diagnostics;
using SpawnerSystem.Shared;

namespace SpawnerSystem.ObjectPooling
{
    internal class PrioritizedPoolSelectorProvider : PoolSelectorProvider
    {
        [SerializeField]
        private List<Entry> priorities;

        private PrioritizedSelector selector;
        public override ISelector PoolSelector
        {
            get
            {
                if (selector is null)
                {
                    int[] priorities = GetPriorities();
                    selector = new PrioritizedSelector(priorities);
                }

                return selector;
            }
        }

        private int[] GetPriorities()
        {
            int length = priorities.Count;
            int[] result = new int[length];
            result[0] = priorities[0].Priority;

            for (int i = 1; i < length; i++)
                result[i] = priorities[i].Priority + result[i - 1];

            return result;
        }

        public override void Initialize<T>(PoolPreparer<T>[] poolPreparers, MultiPoolPreparer<T>[] multiPoolPreparers)
        {
            foreach (var preparer in poolPreparers)
            {
                var preparerObject = preparer.gameObject;
                if (!priorities.Any(entry => entry.PoolProvider == preparerObject))
                    priorities.Add(new Entry(preparerObject));
            }
            foreach (var preparer in multiPoolPreparers)
            {
                var preparerObject = preparer.gameObject;
                if (!priorities.Any(entry => entry.PoolProvider == preparerObject))
                    priorities.Add(new Entry(preparerObject));
            }
        }

        [Serializable]
        private struct Entry
        {
            public GameObject PoolProvider;
            public int Priority;

            public Entry(GameObject provider)
            {
                PoolProvider = provider;
                Priority = 0;
            }
        }
    }
}