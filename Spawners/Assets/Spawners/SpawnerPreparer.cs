﻿using UnityEngine;
using System.Collections;
using System.Linq;
using SpawnerSystem.ObjectPooling;
using SpawnerSystem.Shared;
using System.Diagnostics;
using NaughtyAttributes;

namespace SpawnerSystem.Spawners
{
    internal abstract class SpawnerPreparer<T> : MonoBehaviour where T : Component
    {
        [SerializeField]
        private SpawnPoint[] spawnPoints;

        [SerializeField]
        private SelectorProvider selectorProvider;

        protected abstract MultiPoolPreparer<T> PoolPreparer { get; }

        private Spawner<T> spawner;
        public Spawner<T> Spawner
        {
            get
            {
                if (spawner is null)
                    spawner = CreateSpawner();
                return spawner;
            }
        }


        private Spawner<T> CreateSpawner()
        {
            var pool = PoolPreparer.MultiPool;
            var selector = selectorProvider.Selector;

            return new Spawner<T>(pool, spawnPoints, selector, spawnListeners: null);
        }


        [Conditional("UNITY_EDITOR"), Button]
        protected void InitializeSelector()
        {
            if (selectorProvider == null)
                return;

            GameObject[] spawnPointObjects = spawnPoints.Select(spawnPoint => spawnPoint.gameObject).ToArray();
            selectorProvider.Initialize(spawnPointObjects);
        }

        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            InitializeSelector();
        }
    }
}