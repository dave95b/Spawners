using Experimental.ObjectPooling.Preparer;
using Experimental.ObjectPooling.StateRestorer;
using UnityEngine;

namespace Experimental.Tests
{
    public class TransformPoolPreparer : PoolPreparer<Transform>
    {
        protected override IStateRestorer<Transform> StateRestorer => new TransformPoolStateRestorer();
    }

    class TransformPoolStateRestorer : IStateRestorer<Transform>
    {
        public TransformPoolStateRestorer()
        { }

        public void OnRetrieve(Transform pooled) 
        { }

        public void OnReturn(Transform returned)
        {
            returned.position = Vector3.zero;
        }
    }
}