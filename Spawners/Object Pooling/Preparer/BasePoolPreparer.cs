using UnityEngine;

namespace ObjectManagement.ObjectPooling.Preparer
{
    public abstract class BasePoolPreparer<T> : MonoBehaviour where T : Component
    {
        internal abstract void CreateObjects();
    }
}