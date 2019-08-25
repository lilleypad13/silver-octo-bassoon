using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveInformation : MonoBehaviour
{
    #region Variables
    private WaveSpawner waveSpawnerReference;

    //public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;

    #endregion

    #region Unity Methods

    private void Start()
    {
        waveSpawnerReference = WaveSpawner.GetInstance();

        // Reset the wave spawner each time a level loads
        waveSpawnerReference.resetWaveSpawner();

        PassWaveInformation();
        waveSpawnerReference.enabled = true;
    }

    public void PassWaveInformation()
    {
        waveSpawnerReference.setWaves(waves);
        waveSpawnerReference.setSpawnPoint(spawnPoint);
        waveSpawnerReference.setTimeBetweenWaves(timeBetweenWaves);
    }

    #endregion
}
