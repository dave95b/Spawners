using UnityEngine;
using System.Collections.Generic;
using SpawnerSystem.ObjectPooling;

public class TransformPoolableStateRestorer : MonoBehaviour, IPoolableStateResotrer<Transform>
{
    public void OnRetrieve(Poolable<Transform> poolable)
    {
        poolable.gameObject.SetActive(true);
        poolable.Target.localScale = Vector3.one;
    }

    public void OnReturn(Poolable<Transform> poolable)
    {
        poolable.gameObject.SetActive(false);
    }
}
