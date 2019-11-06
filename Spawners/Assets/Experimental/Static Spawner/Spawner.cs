using Experimental.Spawners.Listener;
using UnityEngine;

namespace Experimental.Spawners.Static
{
    public static class Spawner
    {
        private static StaticSpawnerBehaviour spawnerBehaviour;
        private static StaticSpawnerBehaviour SpawnerBehaviour
        {
            get
            {
                if (spawnerBehaviour == null)
                {
                    var go = new GameObject(typeof(StaticSpawnerBehaviour).Name);
                    spawnerBehaviour = go.AddComponent<StaticSpawnerBehaviour>();
                    spawnerBehaviour.Init();
                }

                return spawnerBehaviour;
            }
        }

        public static T Spawn<T>(T prefab) where T : Component
        {
            return SpawnerBehaviour.Spawn(prefab);
        }

        public static T Spawn<T>(T prefab, in Vector3 position) where T : Component
        {
            return SpawnerBehaviour.Spawn(prefab, position);
        }

        public static T Spawn<T>(T prefab, in Vector3 position, Transform parent) where T : Component
        {
            return SpawnerBehaviour.Spawn(prefab, position, parent);
        }

        public static T Spawn<T>(T prefab, in Vector3 position, in Quaternion rotation, Transform parent) where T : Component
        {
            return SpawnerBehaviour.Spawn(prefab, position, rotation, parent);
        }


        public static void Despawn<T>(T spawned) where T : Component
        {
            SpawnerBehaviour.Despawn(spawned);
        }

        public static void DespawnAll<T>(T prefab) where T : Component
        {
            SpawnerBehaviour.DespawnAll(prefab);
        }


        public static void AddSpawnListener<T>(T prefab, ISpawnListener<T> listener) where T : Component
        {
            SpawnerBehaviour.AddSpawnListener(prefab, listener);
        }

        public static void RemoveSpawnListener<T>(T prefab, ISpawnListener<T> listener) where T : Component
        {
            SpawnerBehaviour.RemoveSpawnListener(prefab, listener);
        }


        public static ISpawner<T> GetSpawnerForPrefab<T>(T prefab) where T : Component
        {
            return SpawnerBehaviour.GetSpawnerForPrefab(prefab);
        }

        public static ISpawner<T> GetSpawnerForSpawned<T>(T spawned) where T : Component
        {
            return SpawnerBehaviour.GetSpawnerForSpawned(spawned);
        }
    }
}