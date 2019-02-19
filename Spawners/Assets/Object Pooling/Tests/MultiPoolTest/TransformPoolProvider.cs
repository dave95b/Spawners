using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

internal class TransformPoolProvider : PoolProvider<Transform>
{
    [SerializeField]
    private TransformPoolPreparer preparer;

    public override IPool<Transform> Pool => preparer.Pool;
}
