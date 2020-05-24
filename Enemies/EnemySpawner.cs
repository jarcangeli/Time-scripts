using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyPrefabs = null;

    Vector2[] spawnPositions;
    float lastSpawn = 0f;
    [SerializeField]
    float spawnDelay = 3f;
    [SerializeField]
    int spawnNumber = 3;
    float spawnRadius = 3f;

    void Awake()
    {
        SetSpawnPositions();
    }

    void SetSpawnPositions()
    {
        spawnPositions = new Vector2[11];
        for (int i = 0; i < 11; ++i)
        {
            float theta = -90f + (i + 1) * 360f / 12f;
            float x = spawnRadius * Mathf.Cos(theta / 360f * 2 * Mathf.PI);
            float y = spawnRadius * Mathf.Sin(theta / 360f * 2 * Mathf.PI);
            spawnPositions[i] = new Vector2(x, y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawn + spawnDelay)
        {
            SpawnEnemies();
            lastSpawn = Time.time;
        }
    }

    void SpawnEnemies()
    {
        List<int> spawnIndices = new List<int>();
        for (int ind = 0; ind < spawnPositions.Length; ++ind) { spawnIndices.Add(ind); }

        for (int i = 0; i < spawnNumber && i < spawnIndices.Count; ++i)
        {
            // get a random enemy type
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);

            // find a unique position
            int ind = spawnIndices[Random.Range(0, spawnIndices.Count)];
            spawnIndices.Remove(ind);
            enemy.transform.position = spawnPositions[ind];
        }
    }
}
