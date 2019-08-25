using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables
    private SceneManagerLite sceneManagerLiteReference;

    #endregion


    #region Unity Methods

    private void Start()
    {
        sceneManagerLiteReference = SceneManagerLite.GetInstance();
    }

    public void Play()
    {
        sceneManagerLiteReference.Play();
    }

    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }

    #endregion

}



