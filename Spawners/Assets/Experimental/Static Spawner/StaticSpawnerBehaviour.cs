using Experimental.ObjectPooling.Builder;
using Experimental.Spawners.Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Experimental.Spawners.Static
{
    internal class StaticSpawnerBehaviour : MonoBehaviour
    {
        private Dictionary<Component, object> prefabsToSpawners, spawnedToSpawners;
        private Dictionary<Component, Transform> parents;

        internal void Init()
        {
            prefabsToSpawners = new Dictionary<Component, object>();
            spawnedToSpawners = new Dictionary<Component, object>();
            parents = new Dictionary<Component, Transform>();
        }


        public T Spawn<T>(T prefab) where T : Component
        {
            var spawner = GetSpawnerForPrefab(prefab);
            T spawned = spawner.Spawn() as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }

        public T Spawn<T>(T prefab, in Vector3 position) where T : Component
        {
            var spawner = GetSpawnerForPrefab(prefab);
            T spawned = spawner.Spawn(position) as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }

        public T Spawn<T>(T prefab, in Vector3 position, Transform parent) where T : Component
        {
            var spawner = GetSpawnerForPrefab(prefab);
            T spawned = spawner.Spawn(position, parent) as T;
            spawnedToSpawners[spawned] = spawner;

            return spawned;
        }

        public T Spawn<T>(T prefab, in Vector3 position, in Quaternion rotation, Transform parent) where T : Component
        {
            var spawner = GetSpawnerForPrefab(prefab);
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
            var spawner = GetSpawnerForPrefab(prefab);
            spawner.DespawnAll();
        }


        public void AddSpawnListener<T>(T prefab, ISpawnListener<T> listener) where T : Component
        {
            var spawner = GetSpawnerForPrefab(prefab);
            spawner.AddSpawnListener(listener);
        }

        public void RemoveSpawnListener<T>(T prefab, ISpawnListener<T> listener) where T : Component
        {
            var spawner = GetSpawnerForPrefab(prefab);
            spawner.RemoveSpawnListener(listener);
        }


        public ISpawner<T> GetSpawnerForPrefab<T>(T prefab) where T : Component
        {
            if (prefabsToSpawners.TryGetValue(prefab, out var spawnerObj))
                return spawnerObj as ISpawner<T>;
            else
                return CreateSpawnerFor(prefab);
        }

        public ISpawner<T> GetSpawnerForSpawned<T>(T spawned) where T : Component
        {
            return spawnedToSpawners[spawned] as ISpawner<T>;
        }


        private ISpawner<T> CreateSpawnerFor<T>(T prefab) where T : Component
        {
            var parent = GetParentFor(prefab);
            var poolBuilder = new ComponentPoolBuilder<T>(parent, prefab);
            var pool = poolBuilder.Build();

            var spawner = new Spawner<T>(pool);
            prefabsToSpawners[prefab] = spawner;

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