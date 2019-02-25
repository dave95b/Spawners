using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.Spawners;

public class TransformSpawnListenerRepository : SpawnListenerRepository<Transform>
{
    [SerializeField]
    private TransformSpawnListener[] spawnListeners;
    public override ISpawnListener<Transform>[] Listeners => spawnListeners;
}
