using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

public class MultiPoolTest : MonoBehaviour
{
    [SerializeField]
    private TransformMultiPoolPreparer preparer;

    [SerializeField]
    private int retrieveCount = 5;

    private MultiPool<Transform> multiPool;
    private Poolable<Transform> poolable;
    private Poolable<Transform>[] poolables;

    private void Awake()
    {
        poolables = new Poolable<Transform>[16];
        multiPool = preparer.MultiPool;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            poolable = multiPool.Retrieve();
            Use(poolable);
        }
        if (Input.GetKeyDown(KeyCode.W))
            RetrieveMany();
    }


    private void Use(Poolable<Transform> poolable)
    {
        StartCoroutine(DelayReturn(poolable));

        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-2f, 2f);
        float z = Random.Range(-5f, 5f);

        Transform pooledTransform = poolable.Target;
        pooledTransform.position = new Vector3(x, y, z);
    }

    private void RetrieveMany()
    {
        if (retrieveCount > poolables.Length)
            poolables = new Poolable<Transform>[retrieveCount];

        multiPool.RetrieveMany(poolables, retrieveCount);

        for (int i = 0; i < retrieveCount; i++)
            Use(poolables[i]);
    }

    private IEnumerator DelayReturn(Poolable<Transform> poolable)
    {
        yield return new WaitForSeconds(2f);
        multiPool.Return(poolable);
    }
}
