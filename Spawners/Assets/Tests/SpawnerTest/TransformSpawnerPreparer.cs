using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.Spawners;
using SpawnerSystem.ObjectPooling;

public class TransformSpawnerPreparer : SpawnerPreparer<Transform>
{
    [SerializeField]
    private TransformMultiPoolPreparer poolPreparer;
    protected override MultiPoolPreparer<Transform> PoolPreparer => poolPreparer;

    protected override List<ISpawnListener<Transform>> SpawnListeners
    {
        get
        {
            var listeners = GetComponentsInChildren<ISpawnListener<Transform>>();
            return new List<ISpawnListener<Transform>>(listeners);
        }
    }
}
