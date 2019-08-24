using System.Collections.Generic;
using UnityEngine;

namespace SpawnerSystem.Shared
{
    public class PrioritizedSelector : ISelector
    {
        private List<float> priorities;

        public PrioritizedSelector(IReadOnlyList<float> priorities)
        {
            this.priorities = new List<float>(priorities.Count);
            CreateStackedPriorities(priorities);
        }

        public void ChangePriorities(IReadOnlyList<float> priorities)
        {
            CreateStackedPriorities(priorities);
        }

        private void CreateStackedPriorities(IReadOnlyList<float> newPriorities)
        {
            int count = newPriorities.Count;
            priorities.Clear();
            priorities.Add(newPriorities[0]);

            for (int i = 1; i < count; i++)
                priorities.Add(newPriorities[i] + priorities[i - 1]);
        }

        public int SelectIndex()
        {
            int min = 0;
            int max = priorities.Count;
            float value = Random.Range(0, priorities[max - 1]);

            while (min < max)
            {
                int middle = ((max - min) / 2) + min;
                float middleValue = priorities[middle];

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