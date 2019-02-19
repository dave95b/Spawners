using UnityEngine;
using System.Collections;

namespace ObjectPooling
{
    public abstract class PoolProvider<T> : MonoBehaviour
    {
        public IPool<T> Pool { get; }
    }
}