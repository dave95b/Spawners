using UnityEngine;
using System;
using System.Collections.Generic;

namespace ObjectPooling
{
    [Serializable]
    internal class PoolExpander
    {
        private readonly PoolData data;
        private readonly int expandAmount, instantiatedPerFrame;
        private readonly MonoBehaviour coroutineHolder;

        public PoolExpander(PoolData data, int expandAmount, int instantiatedPerFrame, MonoBehaviour coroutineHolder)
        {
            this.data = data;
            this.expandAmount = expandAmount;
            this.instantiatedPerFrame = instantiatedPerFrame;
            this.coroutineHolder = coroutineHolder;
        }


        public void Expand()
        {

        }

        public void Instantiate(int amount)
        {

        }
    }
}