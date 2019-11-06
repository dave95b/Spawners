using System;

namespace Experimental.ObjectPooling.Factory
{
    public class PooledDelegateFactory<T> : IPooledFactory<T>
    {
        private readonly Func<T> factory;

        public PooledDelegateFactory(Func<T> factory)
        {
            this.factory = factory;
        }

        public T Create()
        {
            return factory();
        }
    }
}