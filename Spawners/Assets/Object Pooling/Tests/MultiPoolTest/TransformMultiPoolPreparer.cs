using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

internal class TransformMultiPoolPreparer : MultiPoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolProvider[] providers;
    protected override PoolProvider<Transform>[] PoolProviders => providers;

    protected override void FindPoolProviders()
    {
        providers = GetComponentsInChildren<TransformPoolProvider>();
    }
}
