﻿using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.ObjectPooling;
using NaughtyAttributes;
using System.Linq;

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
    protected override IPoolableStateResotrer<Transform> StateRestorer => restorer;

    protected override void FindPoolPreparers()
    {
        poolPreparers = GetComponentsInChildren<TransformPoolPreparer>().Where(PreparersPredicate).ToArray();
        multiPoolPreparers = GetComponentsInChildren<TransformMultiPoolPreparer>().Where(PreparersPredicate).ToArray();
    }
}