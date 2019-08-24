using SpawnerSystem.Spawners;
using UnityEngine;

public class TransformSpawnLogger : MonoBehaviour, ISpawnListener<Transform>
{
    public void OnSpawned(Transform spawned)
    {
        Debug.Log($"{spawned.name} has just spawned!");
    }

    public void OnDespawned(Transform despawned)
    {
        Debug.Log($"{despawned.name} has just despawned!");
    }
}
