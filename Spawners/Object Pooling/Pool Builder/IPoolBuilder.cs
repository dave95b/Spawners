﻿using ObjectManagement.ObjectPooling.Factory;
using ObjectManagement.ObjectPooling.StateRestorer;
using System.Collections.Generic;

namespace ObjectManagement.ObjectPooling.Builder
{
    public interface IPoolBuilder<T>
    {
        IPoolBuilder<T> WithStateRestorer(IStateRestorer<T> stateRestorer);
        IPoolBuilder<T> WithFactory(IPooledFactory<T> factory);
        IPoolBuilder<T> WithExpandAmount(int toExpand);
        IPoolBuilder<T> WithInitialItems(int expandedAmount);
        IPool<T> Build();
        IPool<T> Build(List<T> pooled);
    }
}