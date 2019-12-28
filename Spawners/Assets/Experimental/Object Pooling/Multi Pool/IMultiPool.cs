using SpawnerSystem.Shared;
using System.Collections.Generic;

namespace Experimental.ObjectPooling
{
    public interface IMultiPool<T> : IPool<T>
    {
        IEnumerable<IPool<T>> Pools { get; }
        ISelector Selector { get; set; }

        T RetrieveFrom(int index);
        T RetrieveFrom(IPool<T> pool);
    }
}