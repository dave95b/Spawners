using UnityEngine;
using System.Collections;

namespace ObjectPooling
{
    internal abstract class PoolProvider : MonoBehaviour { }

    internal abstract class PoolProvider<T> : PoolProvider
    {
        public abstract IPool<T> Pool { get; }
    }
}