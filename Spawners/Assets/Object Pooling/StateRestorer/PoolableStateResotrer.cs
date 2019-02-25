using UnityEngine;
using System.Collections.Generic;

namespace SpawnerSystem.ObjectPooling
{
    public interface IPoolableStateResotrer<T>
    {
        void Restore(T target);
    }
}