using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public class MultiPool<T> : IPool<T> where T : Component
    {

        public Poolable<T> Retrieve()
        {
            throw new System.NotImplementedException();
        }

        public void RetrieveMany(Poolable<T>[] poolables)
        {
            throw new System.NotImplementedException();
        }

        public void RetrieveMany(Poolable<T>[] poolables, int count)
        {
            throw new System.NotImplementedException();
        }

        public void Return(Poolable<T> poolable)
        {
            throw new System.NotImplementedException();
        }

        public void ReturnMany(Poolable<T>[] poolables)
        {
            throw new System.NotImplementedException();
        }

        public void ReturnMany(Poolable<T>[] poolables, int count)
        {
            throw new System.NotImplementedException();
        }

        public void ReturnAll()
        {
            throw new System.NotImplementedException();
        }
    }
}