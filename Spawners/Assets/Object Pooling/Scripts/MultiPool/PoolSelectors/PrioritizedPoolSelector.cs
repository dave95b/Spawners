using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ObjectPooling
{
    public class PrioritizedPoolSelector : IMultiPoolSelector
    {
        private readonly int[] priorities;

        public PrioritizedPoolSelector(int[] priorities)
        {
            this.priorities = priorities;
        }

        public int SelectPoolIndex()
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