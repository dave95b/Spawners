using NaughtyAttributes;
using UnityEngine;

namespace RandomSelection
{
    public class SequenceSelectorProvider : SelectorProvider
    {
        [SerializeField, ReadOnly]
        private int objectCount;

        [SerializeField]
        private int step = 1;

        [SerializeField]
        private bool pingPong;

        private SequenceSelector selector;
        public override ISelector Selector
        {
            get
            {
                if (selector is null)
                    selector = new SequenceSelector(objectCount, step, pingPong);

                return selector;
            }
        }

        public override void Initialize(GameObject[] gameObjects)
        {
            objectCount = gameObjects.Length;
        }

        private void OnValidate()
        {
            if (selector != null)
            {
                selector.PingPong = pingPong;
                selector.Step = step;
            }
        }
    }
}