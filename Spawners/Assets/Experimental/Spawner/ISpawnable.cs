using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experimental.Spawner
{
    public interface ISpawnable
    {
        void OnSpawned();
        void OnDespawned();
    }
}