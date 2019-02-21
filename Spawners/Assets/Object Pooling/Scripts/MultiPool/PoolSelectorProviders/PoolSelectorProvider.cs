using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ObjectPooling
{
    internal abstract class PoolSelectorProvider : MonoBehaviour
    {
        public abstract IMultiPoolSelector PoolSelector { get; }
        [Conditional("UNITY_EDITOR")]
        public abstract void Initialize<T>(PoolPreparer<T>[] poolPreparers, MultiPoolPreparer<T>[] multiPoolPreparers);
    }
}