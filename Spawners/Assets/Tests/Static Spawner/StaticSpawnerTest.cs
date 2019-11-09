using Experimental.Spawners.Static;
using System.Collections;
using UnityEngine;

namespace Experimental.Tests
{
    public class StaticSpawnerTest : MonoBehaviour
    {
        [SerializeField]
        private Move prefab;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private float despawnDelay = 2f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Spawn();
            else if (Input.GetKeyDown(KeyCode.W))
                DespawnAll();
        }

        private void Spawn()
        {
            var spawned = Spawner.Spawn(prefab, spawnPoint.position);
            StartCoroutine(DespawnWithDelay(spawned));
        }

        private IEnumerator DespawnWithDelay(Move spawned)
        {
            yield return new WaitForSeconds(despawnDelay);
            Spawner.Despawn(spawned);
        }

        private void DespawnAll()
        {
            StopAllCoroutines();
            Spawner.DespawnAll(prefab);
        }
    }
}