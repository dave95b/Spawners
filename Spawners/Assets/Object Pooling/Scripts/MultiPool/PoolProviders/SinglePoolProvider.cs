using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    internal abstract class SinglePoolProvider<T> : PoolProvider<T>
    {
        protected abstract PoolPreparer<T> PoolPreparer { get; }
        public override IPool<T> Pool => PoolPreparer.Pool;
    }
}