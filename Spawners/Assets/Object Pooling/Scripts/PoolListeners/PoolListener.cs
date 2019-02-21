using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class PoolListener<T>
    {
        public abstract void OnRetrieved(Poolable<T> poolable);
        public abstract void OnReturned(Poolable<T> poolable);
    }
}