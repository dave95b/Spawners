﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experimental.Spawner.Listener
{
    public interface ISpawnListener<in T>
    {
        void OnSpawned(T spawned);
        void OnDespawned(T despawned);
    }
}