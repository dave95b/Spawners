using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    internal abstract class PoolSelectorProvider : MonoBehaviour
    {
        [SerializeField]
        protected MultiPoolPreparer preparer;

        public abstract IMultiPoolSelector PoolSelector { get; }
    }
}