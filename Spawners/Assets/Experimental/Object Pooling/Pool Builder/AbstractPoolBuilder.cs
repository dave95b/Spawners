using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Experimental.ObjectPooling.Builder
{
    public abstract class AbstractPoolBuilder<T> : IPoolBuilder<T>
    {
        protected int toExpand = 0, expandAmount = 1;
        protected IPooledFactory<T> factory;
        protected IStateRestorer<T> stateRestorer;

        protected abstract IStateRestorer<T> DefaultStateRestorer { get; }
        protected abstract IPooledFactory<T> DefaultFactory { get; }


        public IPool<T> Build()
        {
            List<T> pooled = new List<T>(toExpand);
            return Build(pooled);
        }

        public IPool<T> Build(List<T> pooled)
        {
            stateRestorer = stateRestorer ?? DefaultStateRestorer;
            factory = factory ?? DefaultFactory;

            for (int i = 0; i < toExpand; i++)
            {
                T created = factory.Create();
                pooled.Add(created);
            }

            return new Pool<T>(stateRestorer, factory, expandAmount, pooled);
        }

        public IPoolBuilder<T> Expanded(int toExpand)
        {
            Assert.IsTrue(toExpand >= 0);
            this.toExpand = toExpand;
            return this;
        }

        public IPoolBuilder<T> WithExpandAmount(int expandAmount)
        {
            Assert.IsTrue(expandAmount > 0);
            this.expandAmount = expandAmount;
            return this;
        }

        public IPoolBuilder<T> WithFactory(IPooledFactory<T> factory)
        {
            this.factory = factory;
            return this;
        }

        public IPoolBuilder<T> WithStateRestorer(IStateRestorer<T> stateRestorer)
        {
            this.stateRestorer = stateRestorer;
            return this;
        }
    }
}