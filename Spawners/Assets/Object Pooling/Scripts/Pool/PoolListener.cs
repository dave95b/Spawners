using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class PoolListener<T> where T : Component
    {
        public abstract void OnRetrieved(Poolable<T> poolable);
        public abstract void OnReturned(Poolable<T> poolable);
    }
}