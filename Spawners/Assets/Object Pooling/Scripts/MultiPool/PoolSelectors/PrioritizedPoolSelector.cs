using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class PrioritizedPoolSelector<T> : MultiPoolSelector<T> where T : Component
    {
        private readonly int[] priorities;

        public PrioritizedPoolSelector(IPool<T>[] pools, int[] priorities) : base(pools)
        {
            Assert.AreEqual(pools.Length, priorities.Length);
            this.priorities = priorities;
        }

        public override IPool<T> SelectPool()
        {
            int index = SelectIndex();
            return pools[index];
        }

        private int SelectIndex()
        {
            int min = 0;
            int max = priorities.Length;
            int value = Random.Range(0, priorities[max - 1]);

            while (min < max)
            {
                int middle = ((max - min) / 2) + min;
                int middleValue = priorities[middle];

                if (value >= middleValue)
                    min = middle;
                else
                {
                    if (middle == 0 || value >= priorities[middle - 1])
                        return middle;
                    else
                        max = middle;
                }
            }

            return 0;
        }
    }
}