using System.Diagnostics;
using UnityEngine;
using SpawnerSystem.Shared;

namespace SpawnerSystem.ObjectPooling
{
    internal abstract class PoolSelectorProvider : MonoBehaviour
    {
        public abstract ISelector PoolSelector { get; }
        [Conditional("UNITY_EDITOR")]
        public abstract void Initialize<T>(PoolPreparer<T>[] poolPreparers, MultiPoolPreparer<T>[] multiPoolPreparers);
    }
}