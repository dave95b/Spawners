using UnityEngine;
using System.Collections.Generic;
using System;

namespace SpawnerSystem.Spawners
{
    public abstract class SpawnListenerRepository<T> where T : Component
    {
        public abstract ISpawnListener<T>[] Listeners { get; }
    }
}