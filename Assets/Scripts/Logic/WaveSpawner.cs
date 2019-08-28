using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    #region Variables

    private PlayerStats playerStatsReference;
    static WaveSpawner waveSpawnerInstance;


    public int enemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;

    public Text waveCountdownText;
    public GameManager gameManager;

    public float countdown = 2f;
    public int waveIndex = 0;

    IEnumerator spawnWave;
    private bool wavesFinished = false;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (waveSpawnerInstance != null)
        {
            Debug.Log("More than one Wave Spawner Instance Lite in scene");
            return;
        }
        waveSpawnerInstance = this;

        playerStatsReference = PlayerStats.GetInstance();

        wavesFinished = false;
    }

    private void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }

        if (waveIndex >= waves.Length)
        {
            return;
        }

        if (countdown <= 0f)
        {
            spawnWave = SpawnWave();
            StartCoroutine(spawnWave);
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        Debug.Log("The number of waves in total is: " + waves.Length);
    }

    public static WaveSpawner GetInstance()
    {
        return waveSpawnerInstance;
    }

    IEnumerator SpawnWave()
    {
        playerStatsReference.AddRound();

        Wave wave = waves[waveIndex];

        enemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1.0f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }


    /*
     * Checks to see if all of the waves have been cycled through yet
     */
    public bool WavesFinished()
    {
        if(waves.Length != 0 && waveIndex == waves.Length)
        {
            wavesFinished = true;
        }

        return wavesFinished;
    }


    /*
     * Allows setting of the Wave array in this object
     */
    public void setWaves(Wave[] wavesArray)
    {
        waves = wavesArray;
    }


    /*
     * Allows setting of the spawn point location for waves
     */
    public void setSpawnPoint(Transform spawnPointLocation)
    {
        spawnPoint = spawnPointLocation;
    }


    /*
     * Allows setting of the time between wave spawns
     */
    public void setTimeBetweenWaves(float time)
    {
        timeBetweenWaves = time;
    }


    /*
     * Deactivates this script specifically
     */
    public void DeactivateWaveSpawner()
    {
        this.enabled = false;
    }

    public void OnDisable()
    {
        if(spawnWave != null)
        {
            StopCoroutine(spawnWave);
        }
    }

    /*
     * Resets this waveSpawner's variables which need reset when a level is loaded
     */
    public void resetWaveSpawner()
    {
        wavesFinished = false;
        enemiesAlive = 0;
        waveIndex = 0;
        countdown = 2f;
    }

    #endregion
}
