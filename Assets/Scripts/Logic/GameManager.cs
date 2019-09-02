using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

    static GameManager gameManagerInstance;

    private bool gameIsOver;

    public int currentLevelBuildIndex;
    public int levelToUnlock;
    public int currentMaxLevelUnlocked;
    private string levelReached = "levelReached";
    #endregion

    #region Unity Methods

    private void Start()
    {
        if (gameManagerInstance != null)
        {
            Debug.Log("More than one Game Manager Instance in scene");
            return;
        }
        gameManagerInstance = this;

        gameIsOver = true;

        currentMaxLevelUnlocked = PlayerPrefs.GetInt(levelReached, 1);
    }

    public static GameManager GetInstance()
    {
        return gameManagerInstance;
    }


    public bool GetGameIsOver()
    {
        return gameIsOver;
    }

    /*
     * Called to start a level
     * Signal in game processes to start
     */
    public void StartGame()
    {
        gameIsOver = false;
    }


    /*
     * Called to end a level
     * Stop in game processes and submit to the UI menus
     */
    public void EndGame()
    {
        gameIsOver = true;
    }

    public void WinLevel()
    {
        UnlockNextLevel();
    }

    public void UnlockNextLevel()
    {
        // levelToUnlock should be some consistent value away from the currentLevelBuildIndex depending on how many other scenes are
        // added to the build index.
        levelToUnlock = currentLevelBuildIndex + 1;

        // This will only update the unlocked levels if the next level is currently unlocked.
        if (levelToUnlock > currentMaxLevelUnlocked)
        {
            PlayerPrefs.SetInt(levelReached, levelToUnlock);
        }

    }

    #endregion
}
