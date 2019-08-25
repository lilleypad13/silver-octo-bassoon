using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInformation : MonoBehaviour
{
    #region Variables

    private int thisLevelBuildIndex;

    private SceneManagerLite sceneManagerLiteReference;

    #endregion

    #region Unity Methods

    private void Start()
    {
        thisLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;

        sceneManagerLiteReference = SceneManagerLite.GetInstance();

        sceneManagerLiteReference.SetCurrentLevelBuildIndex(thisLevelBuildIndex);

        Debug.Log("SceneInformation says thisLevelBuildIndex is: " + thisLevelBuildIndex);
    }

    private void Update()
    {
        thisLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("SceneInformation Update says thisLevelBuildIndex is: " + thisLevelBuildIndex);
    }

    #endregion
}
