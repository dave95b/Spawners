using ObjectManagement.ObjectPooling.Factory;
using ObjectManagement.ObjectPooling.StateRestorer;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ObjectManagement.ObjectPooling.Builder
{
    public abstract class AbstractPoolBuilder<T> : IPoolBuilder<T>
    {
        protected int itemCount = 0, expandAmount = 1;
        protected IPooledFactory<T> factory;
        protected IStateRestorer<T> stateRestorer;

        protected abstract IStateRestorer<T> DefaultStateRestorer { get; }
        protected abstract IPooledFactory<T> DefaultFactory { get; }


        public IPool<T> Build()
        {
            List<T> pooled = new List<T>(itemCount);
            return Build(pooled);
        }

        public virtual IPool<T> Build(List<T> pooled)
        {
            stateRestorer = stateRestorer ?? DefaultStateRestorer;
            factory = factory ?? DefaultFactory;

            for (int i = 0; i < itemCount; i++)
            {
                T created = factory.Create();
                pooled.Add(created);
            }

            return new Pool<T>(stateRestorer, factory, expandAmount, pooled);
        }

        public IPoolBuilder<T> WithInitialItems(int itemCount)
        {
            Assert.IsTrue(itemCount >= 0);
            this.itemCount = itemCount;
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