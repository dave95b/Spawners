using SpawnerSystem.ObjectPooling;
using SpawnerSystem.Spawners;
using System.Collections.Generic;
using UnityEngine;

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
