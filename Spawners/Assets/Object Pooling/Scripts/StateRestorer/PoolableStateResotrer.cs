using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public interface IPoolableStateResotrer<T>
    {
        void Restore(T target);
    }
}