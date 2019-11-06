using UnityEngine;
using System.Collections.Generic;
using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;

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
            stateRestorer = stateRestorer ?? DefaultStateRestorer;
            factory = factory ?? DefaultFactory;

            List<T> pooledObjects = new List<T>(toExpand);
            for (int i = 0; i < toExpand; i++)
            {
                T created = factory.Create();
                pooledObjects.Add(created);
            }

            return new Pool<T>(stateRestorer, factory, expandAmount, pooledObjects);
        }

        public IPoolBuilder<T> Expanded(int toExpand)
        {
            this.toExpand = toExpand;
            return this;
        }

        public IPoolBuilder<T> WithExpandAmount(int expandAmount)
        {
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