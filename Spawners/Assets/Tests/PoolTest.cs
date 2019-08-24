using SpawnerSystem.ObjectPooling;
using System.Collections;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [SerializeField]
    private TransformPoolPreparer poolPreparer;

    [SerializeField]
    private int retrieveCount = 5;

    [SerializeField]
    private float returnDelay = 2f;

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
            Use(poolable);
        }
        if (Input.GetKeyDown(KeyCode.W))
            RetrieveMany();
    }

    private void RetrieveMany()
    {
        if (retrieveCount > poolables.Length)
            poolables = new Poolable<Transform>[retrieveCount];

        pool.RetrieveMany(poolables, retrieveCount);

        for (int i = 0; i < retrieveCount; i++)
            Use(poolables[i]);
    }

    private void Use(Poolable<Transform> poolable)
    {
        StartCoroutine(DelayReturn(poolable));

        float x = Random.Range(-2f, 2f);
        float z = Random.Range(-2f, 2f);

        Transform pooledTransform = poolable.Target;
        pooledTransform.position = new Vector3(x, 0f, z);
    }

    private IEnumerator DelayReturn(Poolable<Transform> poolable)
    {
        yield return new WaitForSeconds(returnDelay);
        pool.Return(poolable);
    }
}
