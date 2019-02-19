using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public class RandomPoolSelector : IMultiPoolSelector
    {
        private readonly int poolCount;

        public RandomPoolSelector(int poolCount)
        {
            this.poolCount = poolCount;
        }

        public int SelectPoolIndex()
        {
            return Random.Range(0, poolCount);
        }
    }
}