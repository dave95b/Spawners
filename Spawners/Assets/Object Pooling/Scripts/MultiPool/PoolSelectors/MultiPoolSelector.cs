using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class MultiPoolSelector<T> where T : Component
    {
        protected readonly IPool<T>[] pools;

        protected MultiPoolSelector(IPool<T>[] pools)
        {
            this.pools = pools;
        }

        public abstract IPool<T> SelectPool();
    }
}