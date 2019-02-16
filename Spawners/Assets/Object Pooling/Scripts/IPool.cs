using UnityEngine;

namespace ObjectPooling
{
    public interface IPool<T> where T : Component
    {
        Poolable<T> Retrieve();
        void RetrieveMany(Poolable<T>[] poolables);
        void RetrieveMany(Poolable<T>[] poolables, int count);

        void Return(Poolable<T> poolable);
        void ReturnMany(Poolable<T>[] poolables);
        void ReturnMany(Poolable<T>[] poolables, int count);
        void ReturnAll();
    }
}