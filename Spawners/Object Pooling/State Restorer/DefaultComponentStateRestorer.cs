using UnityEngine;

namespace ObjectManagement.ObjectPooling.StateRestorer
{
    public class DefaultComponentStateRestorer<T> : IStateRestorer<T> where T : Component
    {
        private readonly Transform parent;
        private readonly IStateRestorer<T> restorer;

        public DefaultComponentStateRestorer(Transform parent) : this(parent, null)
        { }

        public DefaultComponentStateRestorer(Transform parent, IStateRestorer<T> restorer)
        {
            this.parent = parent;
            this.restorer = restorer;
        }

        public void OnRetrieve(T pooled)
        {
            pooled.gameObject.SetActive(true);
            restorer?.OnRetrieve(pooled);
        }

        public void OnReturn(T returned)
        {
            returned.gameObject.SetActive(false);
            returned.transform.SetParent(parent);
            returned.transform.rotation = Quaternion.identity;
            restorer?.OnReturn(returned);
        }
    }
}