using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int currentRound = 0;
    public int enemiesPerWave = 10;
    public float timeBetweenWaves = 30f;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public TextMeshProUGUI roundText;

    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
    private bool waveInProgress;

    // Start is called before the first frame update
    void Start()
    {
        StartRound();
    }

    void Update()
    {
        roundText.text = $"{currentRound}";
    }

    void StartRound()
    {
        currentRound++;
        enemiesRemainingToSpawn = enemiesPerWave;
        enemiesRemainingAlive = enemiesPerWave;
        waveInProgress = true;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        waveInProgress = false;
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        enemiesRemainingToSpawn--;
        Debug.Log("Aparecio un enemigo, quedan " + enemiesRemainingToSpawn + " por aparecer");
    }

    public void OnEnemyKilled()
    {
        enemiesRemainingAlive--;
        Debug.Log("Mataste un enemigo, quedan "+enemiesRemainingAlive + " por destruir");
        if (enemiesRemainingAlive <= 0 && !waveInProgress)
        {
            Debug.Log("Has sobrevivido a la ronda " + currentRound);
            
            Invoke("StartRound",timeBetweenWaves);
        }
    }
}
