using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

internal class TransformMultiPoolProvider : MultiPoolProvider<Transform>
{
    [SerializeField]
    private TransformMultiPoolPreparer preparer;
    protected override MultiPoolPreparer<Transform> PoolPreparer => preparer;
}
