﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experimental.ObjectPooling
{
    public interface IPool<T>
    {
        T Retrieve();
        
        void Return(T pooled);
        void ReturnAll();
    }
}