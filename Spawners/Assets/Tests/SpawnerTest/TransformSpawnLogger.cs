using UnityEngine;
using System.Collections.Generic;

public class TransformSpawnLogger : TransformSpawnListener
{
    public override void OnSpawned(Transform spawned)
    {
        Debug.Log($"{spawned.name} has just spawned!");
    }

    public override void OnDespawned(Transform despawned)
    {
        Debug.Log($"{despawned.name} has just despawned!");
    }
}
