using SpawnerSystem.ObjectPooling;
using UnityEngine;

internal class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;

    [SerializeField]
    private TransformPoolableStateRestorer restorer;
    protected override IPoolableStateRestorer<Transform> StateRestorer => restorer;
}
