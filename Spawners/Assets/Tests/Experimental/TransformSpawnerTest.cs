using UnityEngine;
using System.Collections.Generic;
using Experimental.Spawners;
using System.Collections;

namespace Experimental.Tests
{
    public class TransformSpawnerTest : MonoBehaviour
    {
        [SerializeField]
        private TransformSpawnerPreparer preparer;

        [SerializeField]
        private float despawnDelay = 2f;

        private ISpawner<Transform> Spawner => preparer.Spawner;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Spawn();
            else if (Input.GetKeyDown(KeyCode.W))
                DespawnAll();
        }


        private void Spawn()
        {
            var spawned = Spawner.Spawn();
            StartCoroutine(DespawnWithDelay(spawned));
        }

        private IEnumerator DespawnWithDelay(Transform spawned)
        {
            yield return new WaitForSeconds(despawnDelay);
            Spawner.Despawn(spawned);
        }

        private void DespawnAll()
        {
            StopAllCoroutines();
            Spawner.DespawnAll();
        }
    }
}