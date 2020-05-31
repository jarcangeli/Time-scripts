using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyPrefabs = null;

    Health playerHealth;

    Vector2[] spawnPositions;
    float lastSpawn = 0f;
    float lastSpawnIncrease = 0f;
    [SerializeField]
    float spawnDelay = 6f;
    [SerializeField]
    int spawnNumber = 3;
    [SerializeField]
    int maxSpawnNumber = 5;
    [SerializeField]
    int maxTotalNumber = 10;
    float spawnIncreaseTime = 30f;
    float spawnRadius = 4f;

    [SerializeField] float superTime = 200f;
    [SerializeField] Color superColor = Color.red;

    void Awake()
    {
        lastSpawn = Time.time - spawnDelay + 2f;
        SetSpawnPositions();
        playerHealth = FindObjectOfType<PlayerInputs>().GetComponent<Health>();

        Debug.Log("Calling EnemySpawner Awake");
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
        if (Time.time > lastSpawn + spawnDelay && playerHealth.currHealth > 0)
        {
            SpawnEnemies();
            lastSpawn = Time.time;
        }
        if (Time.time > lastSpawnIncrease + spawnIncreaseTime && spawnNumber < maxSpawnNumber)
        {
            ++spawnNumber;
            lastSpawnIncrease = Time.time;
        }
    }

    void SpawnEnemies()
    {
        int nAlive = CountAliveEnemies();
        Debug.Log(nAlive + " enemies alive");

        List<int> spawnIndices = new List<int>();
        for (int ind = 0; ind < spawnPositions.Length; ++ind) { spawnIndices.Add(ind); }

        for (int i = 0; i < spawnNumber && i < spawnIndices.Count && nAlive + i < maxTotalNumber; ++i)
        {
            // get a random enemy type
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            // supe it up maybe?
            if (UnityEngine.Random.value < Time.time / superTime)
            {
                 Superify(enemy);
            }
            // find a unique position
            int ind = spawnIndices[Random.Range(0, spawnIndices.Count)];
            spawnIndices.Remove(ind);
            enemy.transform.position = spawnPositions[ind];
        }
    }

    void Superify(GameObject enemy)
    {
        if (enemy.GetComponentInChildren<SpriteRenderer>() is SpriteRenderer sprite)
        {
            sprite.color = superColor;
        }
        if (enemy.GetComponent<IAIBehaviour>() is IAIBehaviour enemyAI)
        {
            enemyAI.Superify();
        }
    }

    int CountAliveEnemies()
    {
        return FindObjectsOfType<EnemyController>().Length;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Vector3.zero, spawnRadius);
    }
}
