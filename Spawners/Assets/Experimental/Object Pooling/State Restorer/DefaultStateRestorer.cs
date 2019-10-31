using UnityEngine;
using System.Collections.Generic;

namespace Experimental.ObjectPooling
{
    internal class DefaultStateRestorer<T> : IStateRestorer<T> where T : Component
    {
        private readonly Transform parent;

        public DefaultStateRestorer(Transform parent)
        {
            this.parent = parent;
        }

        public void OnRetrieve(T pooled)
        {
            pooled.gameObject.SetActive(false);
        }

        public void OnReturn(T returned)
        {
            returned.gameObject.SetActive(false);
            returned.transform.SetParent(parent);
            returned.transform.rotation = Quaternion.identity;
        }
    }
}