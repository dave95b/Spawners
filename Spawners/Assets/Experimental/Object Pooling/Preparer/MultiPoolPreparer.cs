using Experimental.ObjectPooling.StateRestorer;
using NaughtyAttributes;
using SpawnerSystem.Shared;
using System.Collections.Generic;
using UnityEngine;

namespace Experimental.ObjectPooling.Preparer
{
    public abstract partial class MultiPoolPreparer<T> : BasePoolPreparer<T>, IPoolPreparer<T> where T : Component
    {
        [SerializeField]
        private SelectorProvider selectorProvider;

        [SerializeField]
        private GameObject[] preparerObjects;

        protected virtual IStateRestorer<T> StateRestorer { get; }

        private IMultiPool<T> pool;
        IPool<T> IPoolPreparer<T>.Pool => MultiPool;
        public IMultiPool<T> MultiPool => pool ?? (pool = CreateMultiPool());

        private IMultiPool<T> CreateMultiPool()
        {
            var pools = GetChildPools();
            var selector = selectorProvider.Selector;
            
            return new MultiPool<T>(pools, selector, StateRestorer);
        }

        private IPool<T>[] GetChildPools()
        {
            var count = preparerObjects.Length;
            var pools = new IPool<T>[count];

            for (int i = 0; i < count; i++)
            {
                var child = transform.GetChild(i);
                var preparer = child.GetComponent<IPoolPreparer<T>>();
                pools[i] = preparer.Pool;
            }

            return pools;
        }
    }

#if UNITY_EDITOR

    public partial class MultiPoolPreparer<T>
    {
        private void OnValidate()
        {
            InitializeSelector();
        }

        [Button]
        protected void InitializeSelector()
        {
            if (selectorProvider == null)
                selectorProvider = GetComponent<SelectorProvider>();

            if (selectorProvider != null)
                selectorProvider.Initialize(preparerObjects);
        }

        [Button]
        protected void FindPoolPreparers()
        {
            int count = transform.childCount;
            var preparers = new List<GameObject>(count);

            for (int i = 0; i < count; i++)
            {
                var child = transform.GetChild(i);
                if (child.TryGetComponent<IPoolPreparer<T>>(out _))
                    preparers.Add(child.gameObject);
            }

            preparerObjects = preparers.ToArray();
        }

        [Button]
        internal override void CreateObjects()
        {
            foreach (var preparerObject in preparerObjects)
            {
                BasePoolPreparer<T> preparer = preparerObject.GetComponent<BasePoolPreparer<T>>();
                preparer.CreateObjects();
            }
        }
    }

#endif
}