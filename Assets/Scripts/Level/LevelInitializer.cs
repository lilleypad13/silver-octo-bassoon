using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    #region Variables

    private GameManager gameManagerReference;
    private LevelManager levelManagerReference;
    #endregion

    #region Unity Methods

    private void Start()
    {
        // Gets a reference to the LevelManager if one is available, and ensures the level is reset through the reference
        //if(LevelManager.GetInstance() != null)
        //{
        //    levelManagerReference = LevelManager.GetInstance();
        //    Debug.Log("Got reference to LevelManager.");
        //    levelManagerReference.StartLevel();
        //}
        gameManagerReference = GameManager.GetInstance();
        levelManagerReference = LevelManager.GetInstance();

        gameManagerReference.StartGame();
        levelManagerReference.EnableCameraController();
    }



    #endregion
}
