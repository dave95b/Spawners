using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.ObjectPooling;
using NaughtyAttributes;

class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;

    [SerializeField]
    private TransformPoolableStateRestorer restorer;
    protected override IPoolableStateResotrer<Transform> StateRestorer => restorer;
}
