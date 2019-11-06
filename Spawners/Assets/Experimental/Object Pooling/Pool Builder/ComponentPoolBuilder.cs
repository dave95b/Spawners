using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;
using UnityEngine;

namespace Experimental.ObjectPooling.Builder
{
    public class ComponentPoolBuilder<T> : AbstractPoolBuilder<T> where T : Component
    {
        private readonly Transform parent;
        private readonly T prefab;

        public ComponentPoolBuilder(Transform parent, T prefab)
        {
            this.parent = parent;
            this.prefab = prefab;
        }

        protected override IStateRestorer<T> DefaultStateRestorer => new DefaultComponentStateRestorer<T>(parent);
        protected override IPooledFactory<T> DefaultFactory => new PooledComponentFactory<T>(prefab, stateRestorer);
    }
}