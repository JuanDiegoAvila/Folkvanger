using System.Collections;
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
    public TextMeshProUGUI roundMessage;
    public GameObject roundCanvas;

    public AudioSource roundStartAudioSource;
    public SoundBasics soundBasics;  // Reference to the SoundBasics script

    public string tagToFind = "towerIsland1"; // El tag que estás buscando
    private GameObject[] taggedObjects;
    private bool roundStarted = false;

    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
    private bool waveInProgress;

    // Start is called before the first frame update
    void Start()
    {
        // Encuentra todos los GameObjects con el tag especificado y almacénalos en el array
        taggedObjects = GameObject.FindGameObjectsWithTag(tagToFind);

        // Suscribirse al evento
        Torre.OnTowerStateChanged += CheckAllTowersComplete;
    }

    void OnDestroy()
    {
        // Cancelar la suscripción al evento
        Torre.OnTowerStateChanged -= CheckAllTowersComplete;
    }

    void Update()
    {
        roundText.text = $"{currentRound}";
    }

    void CheckAllTowersComplete()
    {
        if (roundStarted) return; // Evitar que la ronda inicie más de una vez

        foreach (GameObject obj in taggedObjects)
        {
            Torre torreComponent = obj.GetComponentInChildren<Torre>();

            if (torreComponent != null)
            {
                if (torreComponent.GetEstado() == Torre.Estado.completo)
                {
                    StartRound();
                    roundStarted = true;
                    return;
                }
            }
        }
    }

    void StartRound()
    {
        currentRound++;
        enemiesRemainingToSpawn = enemiesPerWave;
        enemiesRemainingAlive = enemiesPerWave;
        waveInProgress = true;

        // Reducir el volumen de la música de fondo
        if (soundBasics != null)
        {
            soundBasics.SetLoopedVolume(0f); // Ajusta este valor según sea necesario
        }

        // Reproducir el audio de inicio de la ronda
        roundStartAudioSource.Play();

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

    IEnumerator ShowMessage()
    {

        roundMessage.text = "Wave Complete! Prepare for round " + (currentRound + 1);
        roundCanvas.SetActive(true);

        yield return new WaitForSeconds(5f);
        roundCanvas.SetActive(false);
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        enemiesRemainingToSpawn--;
    }

    public void OnEnemyKilled()
    {
        enemiesRemainingAlive--;
        if (enemiesRemainingAlive <= 0 && !waveInProgress)
        {
            StartCoroutine(ShowMessage());
            soundBasics.SetLoopedVolume(1.0f);
            roundStartAudioSource.Stop();
            Invoke("StartRound", timeBetweenWaves);
        }
    }
}
