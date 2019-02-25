using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using SpawnerSystem.Shared;
using SpawnerSystem.ObjectPooling;
using System;

namespace SpawnerSystem.Spawners
{
    public class Spawner<T> : ISpawner<T> where T : Component
    {
        private readonly IPool<T> pool;
        private readonly ISpawnPoint[] spawnPoints;
        private readonly ISelector spawnPointSelector;
        private readonly ISpawnListener<T>[] spawnListeners;

        private readonly Dictionary<T, Poolable<T>> spawnedPoolables;
        private Poolable<T>[] poolableArray;


        public Spawner(IPool<T> pool, ISpawnPoint[] spawnPoints, ISelector spawnPointSelector)
        {
            Assert.IsNotNull(pool);
            Assert.IsNotNull(spawnPoints);
            Assert.IsNotNull(spawnPointSelector);

            this.pool = pool;
            this.spawnPoints = spawnPoints;
            this.spawnPointSelector = spawnPointSelector;

            spawnedPoolables = new Dictionary<T, Poolable<T>>();
            poolableArray = new Poolable<T>[16];
        }

        public Spawner(IPool<T> pool, ISpawnPoint[] spawnPoints, ISelector spawnPointSelector, ISpawnListener<T>[] spawnListeners) : this(pool, spawnPoints, spawnPointSelector)
        {
            this.spawnListeners = spawnListeners;
        }

        
        public T Spawn()
        {
            ISpawnPoint spawnPoint = SelectSpawnPoint();
            return Spawn(spawnPoint);
        }

        public T Spawn(ISpawnPoint spawnPoint)
        {
            Assert.IsNotNull(spawnPoint);

            Poolable<T> poolable = pool.Retrieve();
            Initialize(poolable, spawnPoint);

            return poolable.Target;
        }

        public void SpawnMany(T[] spawnedArray)
        {
            SpawnMany(spawnedArray, spawnedArray.Length);
        }

        public void SpawnMany(T[] spawnedArray, int count)
        {
            Assert.IsNotNull(spawnedArray);
            Assert.IsTrue(count <= spawnedArray.Length);

            CheckPoolableArraySize(count);
            pool.RetrieveMany(poolableArray, count);

            for (int i = 0; i < count; i++)
            {
                ISpawnPoint spawnPoint = SelectSpawnPoint();
                Initialize(i, spawnPoint);
            }
        }

        public void SpawnMany(T[] spawnedArray, ISpawnPoint spawnPoint)
        {
            SpawnMany(spawnedArray, spawnedArray.Length, spawnPoint);
        }

        public void SpawnMany(T[] spawnedArray, int count, ISpawnPoint spawnPoint)
        {
            Assert.IsNotNull(spawnedArray);
            Assert.IsTrue(count <= spawnedArray.Length);

            CheckPoolableArraySize(count);
            pool.RetrieveMany(poolableArray, count);

            for (int i = 0; i < count; i++)
                Initialize(i, spawnPoint);
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

        private ISpawnPoint SelectSpawnPoint()
        {
            int spawnPointIndex = spawnPointSelector.SelectIndex();
            Assert.IsTrue(spawnPointIndex < spawnPoints.Length);

            return spawnPoints[spawnPointIndex];
        }

        private void Initialize(int index, ISpawnPoint spawnPoint)
        {
            Poolable<T> poolable = poolableArray[index];
            Initialize(poolable, spawnPoint);
        }

        private void Initialize(Poolable<T> poolable, ISpawnPoint spawnPoint)
        {
            T spawned = poolable.Target;
            Assert.IsNotNull(spawned);

            spawnPoint.Apply(spawned.transform);
            spawnedPoolables[spawned] = poolable;

            if (spawnListeners != null)
            {
                foreach (var listener in spawnListeners)
                    listener.OnSpawned(spawned);
            }
        }

        private void CheckPoolableArraySize(int count)
        {
            if (poolableArray.Length < count)
                poolableArray = new Poolable<T>[count * 2];
        }
    }
}