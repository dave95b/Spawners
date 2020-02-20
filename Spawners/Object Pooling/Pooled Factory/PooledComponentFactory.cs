using ObjectManagement.ObjectPooling.StateRestorer;
using UnityEngine;
using UnityEngine.Assertions;

namespace ObjectManagement.ObjectPooling.Factory
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
            var created = Object.Instantiate(prefab);
            stateRestorer.OnReturn(created);

            return created;
        }
    }
}