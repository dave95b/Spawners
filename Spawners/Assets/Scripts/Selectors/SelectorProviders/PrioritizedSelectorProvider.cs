using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectManagement.Selector
{
    public partial class PrioritizedSelectorProvider : SelectorProvider
    {
        [SerializeField]
        private List<Entry> priorities;

        private PrioritizedSelector selector;
        public override ISelector Selector
        {
            get
            {
                if (selector is null)
                {
                    var converted = new ConvertedCollection<Entry, float>(priorities, entry => entry.Priority);
                    selector = new PrioritizedSelector(converted);
                }

                return selector;
            }
        }

        public override void Initialize(GameObject[] gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (!priorities.Any(entry => entry.GameObject == obj))
                    priorities.Add(new Entry(obj));
            }
        }

        [Serializable]
        private struct Entry
        {
            public GameObject GameObject;
            public float Priority;

            public Entry(GameObject provider)
            {
                GameObject = provider;
                Priority = 0f;
            }
        }
    }

#if UNITY_EDITOR
    public partial class PrioritizedSelectorProvider
    {
        private void OnValidate()
        {
            for (int i = 0; i < priorities.Count; i++)
            {
                var entry = priorities[i];
                entry.Priority = Mathf.Max(entry.Priority, 0f);
                priorities[i] = entry;
            }

            if (selector != null)
            {
                var converted = new ConvertedCollection<Entry, float>(priorities, entry => entry.Priority);
                selector.ChangePriorities(converted);
            }
        }
    }
#endif
}