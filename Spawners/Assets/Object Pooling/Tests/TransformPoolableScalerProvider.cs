using UnityEngine;
using ObjectPooling;
using System.Collections.Generic;

class TransformPoolableScalerProvider : TransformPoolListenerProvider
{
    [SerializeField]
    private float minScale, maxScale;

    [SerializeField]
    private bool uniform;

    private TransformPoolableScaler scaler;
    public override PoolListener<Transform> Listener
    {
        get
        {
            if (scaler is null)
                scaler = new TransformPoolableScaler(minScale, maxScale, uniform);
            return scaler;
        }
    }
}

public class TransformPoolableScaler : PoolListener<Transform>
{
    private float minScale, maxScale;
    private bool uniform;

    public TransformPoolableScaler(float minScale, float maxScale, bool uniform)
    {
        this.minScale = minScale;
        this.maxScale = maxScale;
        this.uniform = uniform;
    }


    public override void OnRetrieved(Poolable<Transform> poolable)
    {
        poolable.transform.localScale = GenerateScale();
    }

    public override void OnReturned(Poolable<Transform> poolable)
    {
        poolable.transform.localScale = Vector3.one;
    }

    private Vector3 GenerateScale()
    {
        if (uniform)
            return GenerateUniformScale();
        else
            return GenerateNonUniformScale();
    }

    private Vector3 GenerateUniformScale()
    {
        float scale = Random.Range(minScale, maxScale);
        return new Vector3(scale, scale, scale);
    }

    private Vector3 GenerateNonUniformScale()
    {
        float x = Random.Range(minScale, maxScale);
        float y = Random.Range(minScale, maxScale);
        float z = Random.Range(minScale, maxScale);

        return new Vector3(x, y, z);
    }
}
