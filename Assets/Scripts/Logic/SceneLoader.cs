using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Variables

    // Loading Progress: private setter, public getter
    private float _loadingProgress;
    public float LoadingProgress { get { return _loadingProgress; } }

    #endregion


    #region Unity Methods

    public void LoadScene(int sceneToLoadBuildIndex)
    {
        // kick-off the one co-routine to rule them all
        StartCoroutine(LoadScenesInOrder(sceneToLoadBuildIndex));
    }

    private IEnumerator LoadScenesInOrder(int sceneToLoadBuildIndex)
    {
        // LoadSceneAsync() returns an AsyncOperation, 
        // so will only continue past this point when the Operation has finished
        //yield return SceneManager.LoadSceneAsync("Loading");

        // as soon as we've finished loading the loading screen, start loading the game scene
        yield return StartCoroutine(LoadSceneRoutine(sceneToLoadBuildIndex));

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoadBuildIndex));
    }

    private IEnumerator LoadSceneRoutine(int sceneBuildIndex)
    {
        var asyncScene = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);

        // this value stops the scene from displaying when it's finished loading
        asyncScene.allowSceneActivation = false;

        while (!asyncScene.isDone)
        {
            // loading bar progress
            _loadingProgress = Mathf.Clamp01(asyncScene.progress / 0.9f) * 100;

            // scene has loaded as much as possible, the last 10% can't be multi-threaded
            if (asyncScene.progress >= 0.9f)
            {
                // we finally show the scene
                asyncScene.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator UnloadSceneRoutine(int sceneBuildIndex)
    {
        SceneManager.UnloadSceneAsync(sceneBuildIndex);

        yield return null;
    }


    #endregion
}
