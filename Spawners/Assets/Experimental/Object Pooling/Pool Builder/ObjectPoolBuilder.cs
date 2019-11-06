using UnityEngine;
using System.Collections.Generic;
using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;

namespace Experimental.ObjectPooling.Builder
{
    public class ObjectPoolBuilder<T> : AbstractPoolBuilder<T>
    {
        protected override IStateRestorer<T> DefaultStateRestorer => EmptyStateRestorer<T>.Instance;
        protected override IPooledFactory<T> DefaultFactory => null;
    }
}