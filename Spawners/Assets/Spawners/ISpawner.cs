using UnityEngine;
using System.Collections.Generic;

namespace SpawnerSystem.Spawners
{
    public interface ISpawner<T> where T : Component
    {
        T Spawn();
        T SpawnAt(ISpawnPoint spawnPoint);
        void Despawn(T spawned);
    }
}
