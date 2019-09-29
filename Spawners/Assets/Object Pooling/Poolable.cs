using NaughtyAttributes;
using UnityEngine;

namespace SpawnerSystem.ObjectPooling
{
    public abstract class Poolable<T> : MonoBehaviour
    {
        [SerializeField]
        public T Target;

        public Pool<T> Pool;
        [ReadOnly]
        public bool IsUsed;


        private void Reset()
        {
            Target = GetComponent<T>();
        }

        public static implicit operator T(Poolable<T> poolable) => poolable.Target;
    }
}