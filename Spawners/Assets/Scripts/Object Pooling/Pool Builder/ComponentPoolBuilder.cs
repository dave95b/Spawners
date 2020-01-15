using ObjectManagement.ObjectPooling.Factory;
using ObjectManagement.ObjectPooling.StateRestorer;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectManagement.ObjectPooling.Builder
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

        protected override IStateRestorer<T> DefaultStateRestorer => new DefaultComponentStateRestorer<T>(parent, stateRestorer);
        protected override IPooledFactory<T> DefaultFactory => new PooledComponentFactory<T>(prefab, stateRestorer);

        public override IPool<T> Build(List<T> pooled)
        {
            stateRestorer = DefaultStateRestorer;
            return base.Build(pooled);
        }
    }
}