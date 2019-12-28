using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

namespace SpawnerSystem.Shared
{
    public abstract partial class PickerSelectorProvider<T> : SelectorProvider
    {
        [SerializeField]
        protected int pickedIndex;

        [SerializeField]
        protected List<T> list;

        private PickerSelector<T> selector;
        public override ISelector Selector => selector ?? (selector = new PickerSelector<T>(list));

        public override void Initialize(GameObject[] gameObjects)
        {

        }
    }

#if UNITY_EDITOR

    partial class PickerSelectorProvider<T>
    {
        [ShowNativeProperty]
        protected T PickedItem => selector == null ? default : selector.PickedItem;

        private void OnValidate()
        {
            if (selector != null)
                selector.PickedIndex = pickedIndex;
        }
    }

#endif
}