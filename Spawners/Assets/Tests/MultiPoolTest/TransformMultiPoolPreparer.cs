using NaughtyAttributes;
using SpawnerSystem.ObjectPooling;
using System.Linq;
using UnityEngine;

public class TransformMultiPoolPreparer : MultiPoolPreparer<Transform>
{
    [SerializeField, ReorderableList]
    private TransformPoolPreparer[] poolPreparers;
    protected override PoolPreparer<Transform>[] PoolPreparers => poolPreparers;

    [SerializeField, ReorderableList]
    private TransformMultiPoolPreparer[] multiPoolPreparers;
    protected override MultiPoolPreparer<Transform>[] MultiPoolPreparers => multiPoolPreparers;

    [SerializeField]
    private TransformPoolableStateRestorer restorer;
    protected override IPoolableStateRestorer<Transform> StateRestorer => restorer;

    protected override void FindPoolPreparers()
    {
        poolPreparers = GetComponentsInChildren<TransformPoolPreparer>().Where(PreparersPredicate).ToArray();
        multiPoolPreparers = GetComponentsInChildren<TransformMultiPoolPreparer>().Where(PreparersPredicate).ToArray();
    }
}
