using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
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

    private void OnEnable()
    {
        // The levelToUnlock value should remain consistent to make sense with the level selection.
        // This variable should be some consistent value away from the currentLevelBuildIndex depending on how many other scenes are
        // added to the build index.
        int levelToUnlock = currentLevelBuildIndex - 2;

        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

    #endregion
}
