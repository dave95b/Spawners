using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

public class TransformListenersRepository : ListenersRepository<Transform>
{
    [SerializeField]
    private TransformPoolListenerProvider[] listenerProviders;
    protected override PoolListenerProvider<Transform>[] ListenerProviders => listenerProviders;
}