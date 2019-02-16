using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public class RandomPoolSelector<T> : MultiPoolSelector<T> where T : Component
    {
        public RandomPoolSelector(IPool<T>[] pools) : base(pools) { }

        public override IPool<T> SelectPool()
        {
            return pools.GetRandom();
        }
    }
}