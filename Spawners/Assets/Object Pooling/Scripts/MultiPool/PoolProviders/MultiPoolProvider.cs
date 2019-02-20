using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    internal abstract class MultiPoolProvider<T> : PoolProvider<T>
    {
        protected abstract MultiPoolPreparer<T> PoolPreparer { get; }
        public override IPool<T> Pool => PoolPreparer.MultiPool;
    }
}