using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCanvasManager : MonoBehaviour
{
    #region Variables

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    // This value will need to be passed in from the actual Level Content scene to get an accurate value with GetActiveScene().buildIndex
    public int currentLevelBuildIndex;

    public int levelSelectorSceneBuildIndex = 3;

    private LevelManager levelManager;
    private SceneManagerLite sceneManagerLiteReference;

    #endregion

    #region Unity Methods

    private void Start()
    {
        sceneManagerLiteReference = SceneManagerLite.GetInstance();
        levelManager = GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ResetOverlayUI()
    {
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        completeLevelUI.SetActive(false);
        levelManager.DeactivateWaveSpawner();
    }

    public void Unpause()
    {
        TogglePauseMenu();
    }

    public void Retry()
    {
        ResetOverlayUI();
        Time.timeScale = 1f;
        sceneManagerLiteReference.RetryLevel();
    }

    public void Menu()
    {
        ResetOverlayUI();
        Time.timeScale = 1f;
        sceneManagerLiteReference.Menu();
    }

    public void NextLevel()
    {
        ResetOverlayUI();
        //Time.timeScale = 1;
        sceneManagerLiteReference.NextLevel();
    }

    #endregion
}
