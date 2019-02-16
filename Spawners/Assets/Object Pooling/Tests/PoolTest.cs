using UnityEngine;
using System.Collections;
using ObjectPooling;

public class PoolTest : MonoBehaviour
{
    [SerializeField]
    private TransformPoolPreparer poolPreparer;

    [SerializeField]
    private int retrieveCount = 5;

    private Pool<Transform> pool;
    private Poolable<Transform> poolable;
    private Poolable<Transform>[] poolables;

    private void Start()
    {
        poolables = new Poolable<Transform>[16];
        pool = poolPreparer.Pool;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            poolable = pool.Retrieve();
            StartCoroutine(DelayReturn(poolable));

            float x = Random.Range(-2f, 2f);
            float z = Random.Range(-2f, 2f);

            Transform pooledTransform = poolable.Target;
            pooledTransform.position = new Vector3(x, 0f, z);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pool.ReturnAll();
            StopAllCoroutines();
        }
        if (Input.GetKeyDown(KeyCode.A))
            RetrieveMany();
        if (Input.GetKeyDown(KeyCode.S))
            ReturnMany();
    }

    private void RetrieveMany()
    {
        if (retrieveCount > poolables.Length)
            poolables = new Poolable<Transform>[retrieveCount];

        pool.RetrieveMany(poolables, retrieveCount);

        for (int i = 0; i < retrieveCount; i++)
            StartCoroutine(DelayReturn(poolables[i]));
    }

    private void ReturnMany()
    {
        StopAllCoroutines();
        pool.ReturnMany(poolables, retrieveCount);
    }


    private IEnumerator DelayReturn(Poolable<Transform> poolable)
    {
        yield return new WaitForSeconds(2f);
        poolable.Return();
    }
}
