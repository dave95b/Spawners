using ObjectManagement.Spawners.Static;
using ObjectManagement.ObjectPooling.StateRestorer;
using System.Collections;
using UnityEngine;

namespace ObjectManagement.Tests
{
    public class StaticSpawnerTest : MonoBehaviour
    {
        [SerializeField]
        private Move prefab;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private float despawnDelay = 2f;

        private void Awake()
        {
            var spawner = Spawner.GetSpawnerForPrefab(prefab);
            var loggerRestorer = new LoggerStateRestorer();
            spawner.Pool.StateRestorer = new DefaultComponentStateRestorer<Move>(transform, loggerRestorer);
        }

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

    class LoggerStateRestorer : IStateRestorer<Move>
    {
        public void OnRetrieve(Move pooled)
        {
            Debug.Log($"On Retrieve {pooled.name}");
        }

        public void OnReturn(Move returned)
        {
            Debug.Log($"On Return {returned.name}");
        }
    }
}