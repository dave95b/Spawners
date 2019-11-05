using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Experimental.Spawner;
using Experimental.ObjectPooling;
using Experimental.ObjectPooling.Factory;
using Experimental.ObjectPooling.StateRestorer;
using Experimental.Spawner.Listener;

namespace Experimental.Spawner.Static
{
    internal class StaticSpawnerBehaviour : MonoBehaviour
    {
        public Move prefab;

        private Dictionary<Component, object> prefabsToSpawners, spawnedToSpawners;
        private Dictionary<Component, Transform> parents;

        private void Awake()
        {
            prefabsToSpawners = new Dictionary<Component, object>();
            spawnedToSpawners = new Dictionary<Component, object>();
            parents = new Dictionary<Component, Transform>();
        }


        public T Spawn<T>(T prefab) where T : Component
        {
            var spawner = GetSpawnerFor(prefab);
            T spawned = spawner.Spawn() as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }

        public T Spawn<T>(T prefab, in Vector3 position) where T : Component
        {
            var spawner = GetSpawnerFor(prefab);
            T spawned = spawner.Spawn(position) as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }

        public T Spawn<T>(T prefab, in Vector3 position, Transform parent) where T : Component
        {
            var spawner = GetSpawnerFor(prefab);
            T spawned = spawner.Spawn(position, parent) as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }

        public T Spawn<T>(T prefab, in Vector3 position, in Quaternion rotation, Transform parent) where T : Component
        {
            var spawner = GetSpawnerFor(prefab);
            T spawned = spawner.Spawn(position, rotation, parent) as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }


        public void Despawn<T>(T spawned) where T : Component
        {
            var spawner = spawnedToSpawners[spawned] as ISpawner<T>;
            spawnedToSpawners.Remove(spawned);

            spawner.Despawn(spawned);
        }

        public void DespawnAll<T>(T prefab) where T : Component
        {
            var spawner = GetSpawnerFor(prefab);
            spawner.DespawnAll();
        }

        public ISpawner<T> GetSpawnerForPrefab<T>(T prefab) where T : Component
        {
            return GetSpawnerFor(prefab) as ISpawner<T>;
        }


        private ISpawner<T> GetSpawnerFor<T>(T prefab) where T : Component
        {
            ISpawner<T> spawner;

            if (prefabsToSpawners.TryGetValue(prefab, out var spawnerObj))
                spawner = spawnerObj as ISpawner<T>;
            else
            {
                var parent = GetParentFor(prefab);
                var stateRestorer = new DefaultStateRestorer<T>(parent);
                var factory = new PooledComponentFactory<T>(prefab, stateRestorer);

                var pool = new Pool<T>(stateRestorer, factory, 1);
                spawner = new Spawner<T>(pool);
                prefabsToSpawners[prefab] = spawner;
            }

            return spawner;
        }

        private Transform GetParentFor<T>(T prefab) where T : Component
        {
            if (parents.TryGetValue(prefab, out var parent))
                return parent;

            parent = new GameObject(prefab.name + " Pool").transform;
            parent.SetParent(transform);
            parents[prefab] = parent;

            return parent;
        }
    }
}