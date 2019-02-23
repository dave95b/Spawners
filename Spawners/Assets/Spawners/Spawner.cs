using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using SpawnerSystem.Shared;
using SpawnerSystem.ObjectPooling;

namespace SpawnerSystem.Spawners
{
    public class Spawner<T> : ISpawner<T> where T : Component
    {
        private readonly IPool<T> pool;
        private readonly ISpawnPoint[] spawnPoints;
        private readonly ISelector spawnPointSelector;
        private readonly ISpawnListener<T>[] spawnListeners;

        private readonly Dictionary<T, Poolable<T>> spawnedPoolables;

        public Spawner(IPool<T> pool, ISpawnPoint[] spawnPoints, ISelector spawnPointSelector, ISpawnListener<T>[] spawnListeners)
        {
            Assert.IsNotNull(pool);
            Assert.IsNotNull(spawnPoints);
            Assert.IsNotNull(spawnPointSelector);

            this.pool = pool;
            this.spawnPoints = spawnPoints;
            this.spawnPointSelector = spawnPointSelector;
            this.spawnListeners = spawnListeners;

            spawnedPoolables = new Dictionary<T, Poolable<T>>();
        }

        
        public T Spawn()
        {
            int spawnPointIndex = spawnPointSelector.SelectIndex();
            Assert.IsTrue(spawnPointIndex < spawnPoints.Length);

            ISpawnPoint spawnPoint = spawnPoints[spawnPointIndex];
            return SpawnAt(spawnPoint);
        }

        public T SpawnAt(ISpawnPoint spawnPoint)
        {
            Assert.IsNotNull(spawnPoint);

            Poolable<T> poolable = pool.Retrieve();
            T spawned = poolable.Target;
            Assert.IsNotNull(spawned);

            spawnPoint.Apply(spawned.transform);
            spawnedPoolables[spawned] = poolable;

            if (spawnListeners != null)
            {
                foreach (var listener in spawnListeners)
                    listener.OnSpawned(spawned);
            }

            return spawned;
        }

        public void Despawn(T spawned)
        {
            Assert.IsNotNull(spawned);

            if (spawnListeners != null)
            {
                foreach (var listener in spawnListeners)
                    listener.OnDespawned(spawned);
            }

            Poolable<T> poolable = spawnedPoolables[spawned];
            pool.Return(poolable);
        }
    }
}