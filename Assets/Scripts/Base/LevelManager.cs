﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Variables

    private GameManager gameManagerReference;
    private WaveSpawner waveSpawnerReference;
    private SceneManagerLite sceneManagerLiteReference;
    private PlayerStats playerStatsReference;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    public CameraController gameCamera;

    static LevelManager levelManagerInstance;
    #endregion

    #region Unity Methods

    private void Start()
    {
        gameManagerReference = GameManager.GetInstance();
        playerStatsReference = PlayerStats.GetInstance();
        waveSpawnerReference = WaveSpawner.GetInstance();


        if (levelManagerInstance != null)
        {
            Debug.Log("More than one LevelManager in scenes.");
            return;
        }
        levelManagerInstance = this;
        // Reset the wave spawner each time a level loads
        //waveSpawnerReference.resetWaveSpawner();

        //StartLevel();
    }

    private void Update()
    {
        if (gameManagerReference.GetGameIsOver())
        {
            return;
        }

        if (playerStatsReference.playerLives <= 0 && gameManagerReference.GetGameIsOver() == false)
        {
            Debug.Log("The player has " + playerStatsReference.playerLives + " lives and has lost.");
            LoseLevel();
        }

        if (waveSpawnerReference.isActiveAndEnabled && waveSpawnerReference.WavesFinished() && 
            waveSpawnerReference.enemiesAlive == 0 && gameManagerReference.GetGameIsOver() == false)
        {
            Debug.Log("The player has won! (Dictated by LevelManager)");
            WinLevel();
        }
    }

    private void LoseLevel()
    {
        EndLevel();
        gameOverUI.SetActive(true);
    }

    private void WinLevel()
    {
        EndLevel();
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


    /*
     * Actions to perform indicating the level has ended
     */
    private void EndLevel()
    {
        gameManagerReference.EndGame();
        waveSpawnerReference.DeactivateWaveSpawner();
        gameCamera.enabled = false;
    }


    /*
     * Activates the Camera Controller
     */
    public void EnableCameraController()
    {
        gameCamera.enabled = true;
    }


    public static LevelManager GetInstance()
    {
        return levelManagerInstance;
    }
    #endregion
}
