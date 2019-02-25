using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SpawnerSystem.Spawners;

public class TransformSpawnerTest : MonoBehaviour
{
    [SerializeField]
    private TransformSpawnerPreparer spawnerPreparer;

    [SerializeField]
    private int spawnCount = 5;

    [SerializeField]
    private SpawnPoint spawnPoint;

    private Spawner<Transform> spawner;
    private Transform[] spawnedArray;

    private void Awake()
    {
        spawner = spawnerPreparer.Spawner;
        spawnedArray = new Transform[10];
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Spawn();
        if (Input.GetKeyDown(KeyCode.W))
            SpawnAt();
        if (Input.GetKeyDown(KeyCode.A))
            SpawnMany();
        if (Input.GetKeyDown(KeyCode.S))
            SpawnManyAt();
        if (Input.GetKeyDown(KeyCode.Z))
            DespawnMany();
    }

    private void Spawn()
    {
        var spawned = spawner.Spawn();
        StartCoroutine(DelayDespawn(spawned));
    }

    private void SpawnAt()
    {
        var spawned = spawner.Spawn(spawnPoint);
        StartCoroutine(DelayDespawn(spawned));
    }

    private void SpawnMany()
    {
        if (spawnCount > spawnedArray.Length)
            spawnedArray = new Transform[spawnCount];

        spawner.SpawnMany(spawnedArray, spawnCount);
    }

    private void SpawnManyAt()
    {
        if (spawnCount > spawnedArray.Length)
            spawnedArray = new Transform[spawnCount];

        spawner.SpawnMany(spawnedArray, spawnCount, spawnPoint);
    }

    private void DespawnMany()
    {
        for (int i = 0; i < spawnCount; i++)
            spawner.Despawn(spawnedArray[i]);
    }

    private IEnumerator DelayDespawn(Transform spawned)
    {
        yield return new WaitForSeconds(2f);
        spawner.Despawn(spawned);
    }
}
