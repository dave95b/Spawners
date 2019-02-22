using UnityEngine;
using System.Collections.Generic;
using ObjectPooling;

public class TransformPoolableStateRestorer : MonoBehaviour, IPoolableStateResotrer<Transform>
{
    public void Restore(Transform target)
    {
        target.localScale = Vector3.one;
    }
}
