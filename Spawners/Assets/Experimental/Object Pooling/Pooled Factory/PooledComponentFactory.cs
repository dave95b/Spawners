using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using Experimental.ObjectPooling.StateRestorer;

namespace Experimental.ObjectPooling.Factory
{
    public class PooledComponentFactory<T> : IPooledFactory<T> where T : Component
    {
        private readonly T prefab;
        private readonly IStateRestorer<T> stateRestorer;

        public PooledComponentFactory(T prefab, IStateRestorer<T> stateRestorer)
        {
            Assert.IsNotNull(prefab);
            Assert.IsNotNull(stateRestorer);

            this.prefab = prefab;
            this.stateRestorer = stateRestorer;
        }

        public virtual T Create()
        {
            var created = GameObject.Instantiate(prefab);
            stateRestorer.OnReturn(created);

            return created;
        }
    }
}