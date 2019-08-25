using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    #region Variables

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

    #endregion
}
