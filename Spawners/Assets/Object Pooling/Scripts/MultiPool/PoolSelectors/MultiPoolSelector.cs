using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public interface IMultiPoolSelector 
    {
        int SelectPoolIndex();
    }
}