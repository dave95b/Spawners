using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;
using NaughtyAttributes;

class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;

    [SerializeField]
    TransformListenersRepository listenersRepository;
    protected override ListenersRepository<Transform> ListenersRepository => listenersRepository;
}

abstract class TransformPoolListenerProvider : PoolListenerProvider<Transform>
{

}
