using SpawnerSystem.ObjectPooling;
using UnityEngine;

public class TransformPoolableStateRestorer : MonoBehaviour, IPoolableStateRestorer<Transform>
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
