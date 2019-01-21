using UnityEngine;
using System.Collections;
using ObjectPooling;

public class PoolTest : MonoBehaviour
{
    public Pool pool;

    private TransformPoolable poolable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            poolable = pool.Retrieve<TransformPoolable>();
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
    }


    private IEnumerator DelayReturn(Poolable poolable)
    {
        yield return new WaitForSeconds(2f);
        poolable.Return();
    }
}
