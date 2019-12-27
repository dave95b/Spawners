using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;

namespace Experimental.ObjectPooling.Builder
{
    public class ObjectPoolBuilder<T> : AbstractPoolBuilder<T> where T : new()
    {
        protected override IStateRestorer<T> DefaultStateRestorer => EmptyStateRestorer<T>.Instance;
        protected override IPooledFactory<T> DefaultFactory => new PooledObjectFactory<T>();
    }
}