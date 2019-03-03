﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SpawnerSystem.ObjectPooling
{
    public abstract class Poolable : MonoBehaviour
    {
    }

    public abstract class Poolable<T> : Poolable
    {
        [SerializeField]
        public T Target;

        public Pool<T> Pool;
        public bool IsUsed;
    }
}