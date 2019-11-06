using UnityEngine;
using System.Collections.Generic;
using Experimental.ObjectPooling;
using Experimental.Spawner.Listener;
using UnityEngine.Assertions;

namespace Experimental.Spawner
{
    public class Spawner<T> : ISpawner<T> where T : Component
    {
        private readonly IPool<T> pool;
        private readonly List<ISpawnListener<T>> spawnListeners;


        public Spawner(IPool<T> pool) : this(pool, new List<ISpawnListener<T>>()) { }

        public Spawner(IPool<T> pool, List<ISpawnListener<T>> spawnListeners)
        {
            Assert.IsNotNull(pool);
            Assert.IsNotNull(spawnListeners);

            this.pool = pool;
            this.spawnListeners = spawnListeners;
        }


        public T Spawn()
        {
            return Spawn(Vector3.zero, Quaternion.identity, null);
        }

        public T Spawn(in Vector3 position)
        {
            return Spawn(position, Quaternion.identity, null);
        }

        public T Spawn(in Vector3 position, Transform parent)
        {
            return Spawn(position, Quaternion.identity, parent);
        }

        public T Spawn(in Vector3 position, in Quaternion rotation, Transform parent)
        {
            T spawned = pool.Retrieve();

            spawned.transform.SetPositionAndRotation(position, rotation);
            if (parent != null)
                spawned.transform.SetParent(parent);
            NotifySpawned(spawned);

            return spawned;
        }


        public void Despawn(T despawned)
        {
            NotifyDespawned(despawned);
            pool.Return(despawned);
        }

        public void DespawnAll()
        {
            foreach (var used in pool.UsedObjects)
                NotifyDespawned(used);

            pool.ReturnAll();
        }


        public void AddSpawnListener(ISpawnListener<T> listener)
        {
            spawnListeners.Add(listener);
        }

        public void RemoveSpawnListener(ISpawnListener<T> listener)
        {
            spawnListeners.Remove(listener);
        }


        private void NotifySpawned(T spawned)
        {
            if (spawned is ISpawnable spawnable)
                spawnable.OnSpawned();

            foreach (var listener in spawnListeners)
                listener.OnSpawned(spawned);
        }

        private void NotifyDespawned(T spawned)
        {
            if (spawned is ISpawnable spawnable)
                spawnable.OnDespawned();

            foreach (var listener in spawnListeners)
                listener.OnDespawned(spawned);
        }
    }
}