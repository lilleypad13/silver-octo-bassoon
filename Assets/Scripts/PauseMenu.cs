using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Variables

    public GameObject pauseMenuUI;
    private SceneManagerLite sceneManagerLiteReference;

    // This value will need to be passed in from the actual Level Content scene to get an accurate value with GetActiveScene().buildIndex
    public int currentLevelBuildIndex;

    public int levelSelectorSceneBuildIndex = 3;

    #endregion

    #region Unity Methods

    private void Start()
    {
        sceneManagerLiteReference = SceneManagerLite.GetInstance();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            ToggleWithTime();
        }
    }

    public void ToggleWithTime()
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

    #endregion
}
