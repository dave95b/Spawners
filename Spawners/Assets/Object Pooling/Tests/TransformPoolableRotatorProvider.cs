using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

class TransformPoolableRotatorProvider : TransformPoolListenerProvider
{
    public override PoolListener<Transform> Listener => new TransformPoolableRotator();
}

public class TransformPoolableRotator : PoolListener<Transform>
{
    public override void OnRetrieved(Poolable<Transform> poolable)
    {
        poolable.Target.eulerAngles = GenerateRotation();
    }

    public override void OnReturned(Poolable<Transform> poolable)
    {
        poolable.Target.eulerAngles = Vector3.zero;
    }

    private Vector3 GenerateRotation()
    {
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        float z = Random.Range(0f, 360f);

        return new Vector3(x, y, z);
    }
}
