using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Variables

    private GameManager gameManagerReference;
    private WaveSpawner waveSpawnerReference;
    private SceneManagerLite sceneManagerLiteReference;
    private PlayerStats playerStatsReference;

    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    #endregion

    #region Unity Methods

    private void Start()
    {
        gameManagerReference = GameManager.GetInstance();
        playerStatsReference = PlayerStats.GetInstance();
        waveSpawnerReference = WaveSpawner.GetInstance();

        // Reset the wave spawner each time a level loads
        //waveSpawnerReference.resetWaveSpawner();

        GameIsOver = false;
    }

    private void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        if (playerStatsReference.playerLives <= 0 && GameIsOver == false)
        {
            Debug.Log("The player has " + playerStatsReference.playerLives + " lives and has lost.");
            LoseLevel();
        }

        if (waveSpawnerReference.isActiveAndEnabled && waveSpawnerReference.WavesFinished() && waveSpawnerReference.enemiesAlive == 0 && GameIsOver == false)
        {
            Debug.Log("The player has won! (Dictated by LevelManager)");
            WinLevel();
        }
    }

    private void LoseLevel()
    {
        GameIsOver = true;
        waveSpawnerReference.DeactivateWaveSpawner();

        gameOverUI.SetActive(true);
    }

    private void WinLevel()
    {
        GameIsOver = true;
        waveSpawnerReference.DeactivateWaveSpawner();

        gameManagerReference.WinLevel();

        completeLevelUI.SetActive(true);
    }

    /*
     * Allows for other scripts to use this one's existing reference to WaveSpawner to deactivate it
     */
    public void DeactivateWaveSpawner()
    {
        waveSpawnerReference.DeactivateWaveSpawner();
    }
    

    #endregion
}
