using ObjectManagement.ObjectPooling;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ObjectManagement.Spawners
{
    public class Spawner<T> : ISpawner<T> where T : Component
    {
        public IPool<T> Pool { get; private set; }

        private readonly List<ISpawnListener<T>> spawnListeners;

        public Spawner(IPool<T> pool) : this(pool, new List<ISpawnListener<T>>())
        {
        }

        public Spawner(IPool<T> pool, List<ISpawnListener<T>> spawnListeners)
        {
            Assert.IsNotNull(pool);
            Assert.IsNotNull(spawnListeners);

            Pool = pool;
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

        public T Spawn(in Vector3 position, in Quaternion rotation)
        {
            return Spawn(position, rotation, null);
        }

        public T Spawn(in Vector3 position, in Quaternion rotation, Transform parent)
        {
            T spawned = Pool.Retrieve();

            spawned.transform.SetPositionAndRotation(position, rotation);
            if (parent != null)
                spawned.transform.SetParent(parent);
            NotifySpawned(spawned);

            return spawned;
        }

        public void Despawn(T despawned)
        {
            NotifyDespawned(despawned);
            Pool.Return(despawned);
        }

        public void DespawnAll()
        {
            foreach (var used in Pool.UsedObjects)
                NotifyDespawned(used);

            Pool.ReturnAll();
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