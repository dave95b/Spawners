using UnityEngine;

namespace Experimental.ObjectPooling.StateRestorer
{
    internal class DefaultComponentStateRestorer<T> : IStateRestorer<T> where T : Component
    {
        private readonly Transform parent;

        public DefaultComponentStateRestorer(Transform parent)
        {
            this.parent = parent;
        }

        public void OnRetrieve(T pooled)
        {
            pooled.gameObject.SetActive(true);
        }

        public void OnReturn(T returned)
        {
            returned.gameObject.SetActive(false);
            returned.transform.SetParent(parent);
            returned.transform.rotation = Quaternion.identity;
        }
    }
}