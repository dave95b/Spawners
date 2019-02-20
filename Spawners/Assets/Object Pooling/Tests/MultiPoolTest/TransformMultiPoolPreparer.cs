using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

internal class TransformMultiPoolPreparer : MultiPoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolProvider[] providers;
    [SerializeField]
    private TransformMultiPoolProvider[] multiPoolProviders;

    private PoolProvider<Transform>[] poolProviders;
    protected override PoolProvider<Transform>[] PoolProviders
    {
        get
        {
            if (poolProviders is null)
            {
                poolProviders = new PoolProvider<Transform>[providers.Length + multiPoolProviders.Length];
                int i = 0;
                for (i = 0; i < providers.Length; i++)
                    poolProviders[i] = providers[i];

                for (int j = 0; j < multiPoolProviders.Length; j++)
                {
                    poolProviders[i] = multiPoolProviders[j];
                    i++;
                }
            }

            return poolProviders;
        }
    }

    protected override void FindPoolProviders()
    {
        providers = GetComponentsInChildren<TransformPoolProvider>();
        multiPoolProviders = GetComponentsInChildren<TransformMultiPoolProvider>();
    }
}
