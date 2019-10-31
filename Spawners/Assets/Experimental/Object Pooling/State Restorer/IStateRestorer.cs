using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experimental.ObjectPooling
{
    public interface IStateRestorer<T>
    {
        void OnRetrieve(T pooled);
        void OnReturn(T returned);
    }
}