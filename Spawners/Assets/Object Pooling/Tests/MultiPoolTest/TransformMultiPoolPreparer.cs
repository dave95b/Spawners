using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;
using NaughtyAttributes;
using System.Linq;

internal class TransformMultiPoolPreparer : MultiPoolPreparer<Transform>
{
    [SerializeField, ReorderableList]
    private TransformPoolPreparer[] poolPreparers;
    protected override PoolPreparer<Transform>[] PoolPreparers => poolPreparers;

    [SerializeField, ReorderableList]
    private TransformMultiPoolPreparer[] multiPoolPreparers;
    protected override MultiPoolPreparer<Transform>[] MultiPoolPreparers => multiPoolPreparers;

    [SerializeField]
    TransformListenersRepository listenersRepository;
    protected override ListenersRepository<Transform> ListenersRepository => listenersRepository;


    protected override void FindPoolPreparers()
    {
        poolPreparers = GetComponentsInChildren<TransformPoolPreparer>().Where(PreparersPredicate).ToArray();
        multiPoolPreparers = GetComponentsInChildren<TransformMultiPoolPreparer>().Where(PreparersPredicate).ToArray();
    }
}
