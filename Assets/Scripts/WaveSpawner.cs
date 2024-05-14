using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;
    public int enemiesPerWave = 10;
    public float timeBetweenEnemies = 3f;
    public float waveCooldown = 30f;

    private int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (enemiesSpawned < enemiesPerWave)
                {
                    SpawnEnemy();
                    enemiesSpawned++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }

            enemiesSpawned = 0; //Reset counter for next wave
            yield return new WaitForSeconds(waveCooldown);
        }
    }

    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);  
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
