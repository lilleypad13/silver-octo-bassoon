using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    #region Variables

    private SceneManagerLite sceneManagerLiteReference;

    public Button[] levelButtons;

    //public int buildIndexLevelSelector;

    #endregion



    #region Unity Methods

    // TODO: This shouldn't need to go through the entire for loop every time you load this scene.
    private void Start()
    {
        sceneManagerLiteReference = SceneManagerLite.GetInstance();

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //buildIndexLevelSelector = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("LevelSelector: buildIndexLevelSelector is set to: " + buildIndexLevelSelector);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            //if(i + buildIndexLevelSelector > levelReached)
            if(i > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select (int levelBuildIndex)
    {
        sceneManagerLiteReference.SelectLevel(levelBuildIndex);
    }



    #endregion
}
