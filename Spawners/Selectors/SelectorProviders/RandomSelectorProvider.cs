using NaughtyAttributes;
using UnityEngine;

namespace RandomSelection
{
    public class RandomSelectorProvider : SelectorProvider
    {
        [SerializeField, ReadOnly]
        private int objectCount;

        private IRandomSelector selector;
        public override ISelector Selector
        {
            get
            {
                if (selector is null)
                    selector = SelectorFactory.Random(objectCount);

                return selector;
            }
        }

        public override void Initialize(GameObject[] gameObjects)
        {
            objectCount = gameObjects.Length;
        }
    }
}