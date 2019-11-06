using Experimental.Spawners.Listener;
using UnityEngine;

namespace Experimental.Spawners
{
    public interface ISpawner<T> where T : Component
    {
        T Spawn();
        T Spawn(in Vector3 position);
        T Spawn(in Vector3 position, Transform parent);
        T Spawn(in Vector3 position, in Quaternion rotation, Transform parent);

        void Despawn(T despawned);
        void DespawnAll();

        void AddSpawnListener(ISpawnListener<T> listener);
        void RemoveSpawnListener(ISpawnListener<T> listener);
    }
}