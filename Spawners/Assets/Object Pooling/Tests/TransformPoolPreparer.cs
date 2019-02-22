using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;
using NaughtyAttributes;

class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;

    protected override IPoolableStateResotrer<Transform> StateRestorer => null;
}
