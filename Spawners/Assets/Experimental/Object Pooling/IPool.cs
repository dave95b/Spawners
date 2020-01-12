using Experimental.ObjectPooling.StateRestorer;
using System.Collections.Generic;

namespace Experimental.ObjectPooling
{
    public interface IPool<T>
    {
        IEnumerable<T> UsedObjects { get; }
        IStateRestorer<T> StateRestorer { get; set; }

        T Retrieve();

        void Return(T pooled);
        void ReturnAll();
    }
}