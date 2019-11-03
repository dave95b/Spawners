using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experimental.ObjectPooling.StateRestorer
{
    public interface IStateRestorer<in T>
    {
        void OnRetrieve(T pooled);
        void OnReturn(T returned);
    }
}