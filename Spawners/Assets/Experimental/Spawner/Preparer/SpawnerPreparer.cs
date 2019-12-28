using Experimental.ObjectPooling.Preparer;
using Experimental.Spawners.Listener;
using System.Collections.Generic;
using UnityEngine;

namespace Experimental.Spawners.Preparer
{
    public class SpawnerPreparer<TSpawn, TPreparer> : MonoBehaviour where TSpawn : Component where TPreparer : IPoolPreparer<TSpawn>
    {
        [SerializeField]
        private TPreparer preparer;

        private ISpawner<TSpawn> spawner;
        public ISpawner<TSpawn> Spawner => spawner ?? (spawner = CreateSpawner());

        private ISpawner<TSpawn> CreateSpawner()
        {
            var listenerArray = GetComponentsInChildren<ISpawnListener<TSpawn>>();
            var listeners = new List<ISpawnListener<TSpawn>>(listenerArray);

            return new Spawner<TSpawn>(preparer.Pool, listeners);
        }
    }
}