using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experimental.ObjectPooling.Factory
{
    public interface IPooledFactory<T>
    {
        T Create();
    }
}