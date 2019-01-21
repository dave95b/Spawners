using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace ObjectPooling
{
    public abstract class Poolable : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        public Pool Pool;

        public void Return()
        {
            Pool.Return(this);
        }
    }

    public abstract class Poolable<T> : Poolable where T : Component
    {
        [SerializeField]
        public T Target;
    }
}