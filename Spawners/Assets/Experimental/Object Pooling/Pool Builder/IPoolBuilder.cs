﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Experimental.ObjectPooling.StateRestorer;
using Experimental.ObjectPooling.Factory;

namespace Experimental.ObjectPooling.Builder
{
    public interface IPoolBuilder<T>
    {
        IPoolBuilder<T> WithStateRestorer(IStateRestorer<T> stateRestorer);
        IPoolBuilder<T> WithFactory(IPooledFactory<T> factory);
        IPoolBuilder<T> WithExpandAmount(int toExpand);
        IPoolBuilder<T> Expanded(int expandedAmount);
        IPool<T> Build();
    }
}