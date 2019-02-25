using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.Spawners;
using SpawnerSystem.ObjectPooling;

public class TransformSpawnerPreparer : SpawnerPreparer<Transform>
{
    [SerializeField]
    private TransformMultiPoolPreparer poolPreparer;
    protected override MultiPoolPreparer<Transform> PoolPreparer => poolPreparer;

    [SerializeField]
    private TransformSpawnListenerRepository listenerRepository;
    protected override SpawnListenerRepository<Transform> ListenerRepository => listenerRepository;
}
