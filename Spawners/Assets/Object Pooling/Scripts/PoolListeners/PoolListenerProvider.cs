using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class PoolListenerProvider<T> : MonoBehaviour
    {
        public abstract PoolListener<T> Listener { get; }
    }
}