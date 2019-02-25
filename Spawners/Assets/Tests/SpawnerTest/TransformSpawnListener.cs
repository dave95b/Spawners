using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.Spawners;

public abstract class TransformSpawnListener : MonoBehaviour, ISpawnListener<Transform>
{
    public abstract void OnDespawned(Transform despawned);
    public abstract void OnSpawned(Transform spawned);
}
