using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SpawnerSystem.ObjectPooling
{
    public abstract class Poolable<T> : MonoBehaviour
    {
        [SerializeField]
        public T Target;

        public Pool<T> Pool;
        
        [ReadOnly]
        public bool IsUsed;
    }
}