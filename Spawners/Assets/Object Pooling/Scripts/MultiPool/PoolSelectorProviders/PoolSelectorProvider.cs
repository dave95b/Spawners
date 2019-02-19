using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    internal abstract class PoolSelectorProvider : MonoBehaviour
    {
        public abstract IMultiPoolSelector PoolSelector { get; }
    }
}