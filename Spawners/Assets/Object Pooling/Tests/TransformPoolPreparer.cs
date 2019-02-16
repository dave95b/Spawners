using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;
using NaughtyAttributes;

class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;

    private List<Poolable<Transform>> pooledObjects = new List<Poolable<Transform>>();
    protected override List<Poolable<Transform>> PooledObjects => pooledObjects;

    [SerializeField]
    TransformPoolListenerProvider[] listenerProviders;

    protected override PoolListener<Transform>[] Listeners
    {
        get
        {
            int length = listenerProviders.Length;
            PoolListener<Transform>[] listeners = new PoolListener<Transform>[length];

            for (int i = 0; i < length; i++)
                listeners[i] = listenerProviders[i].Listener;

            return listeners;
        }
    }
}

abstract class TransformPoolListenerProvider : MonoBehaviour
{
    public abstract PoolListener<Transform> Listener { get; }
}
