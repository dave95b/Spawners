using UnityEngine;

namespace ObjectManagement.Selector
{
    public class RandomSelector : ISelector
    {
        private readonly int objectCount;

        public RandomSelector(int objectCount)
        {
            this.objectCount = objectCount;
        }

        public int SelectIndex()
        {
            return Random.Range(0, objectCount);
        }
    }
}