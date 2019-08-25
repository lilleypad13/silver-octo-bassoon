using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerLite : MonoBehaviour
{
    #region Variables

    // Loading Progress: private setter, public getter
    private float _loadingProgress;
    public float LoadingProgress { get { return _loadingProgress; } }


    public Image img;
    public AnimationCurve curve;

    public int buildIndexToUnload;
    public int buildIndexToLoad;

    public int currentLevelBuildIndex;
    public int baseSceneBuildIndex = 1;
    public int mainMenuBuildIndex = 2;
    public int levelSelectorSceneBuildIndex = 3;

    static SceneManagerLite sceneManagerLiteInstance;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (sceneManagerLiteInstance != null)
        {
            Debug.Log("More than one Scene Manager Lite in scene");
            return;
        }
        sceneManagerLiteInstance = this;

        // This loads the first actual content scene upon starting the game along with the objects in the permanent Logic scene.
        // This should load the Main Menu along with the persistent Logic scene
        LoadScene(mainMenuBuildIndex);
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public static SceneManagerLite GetInstance()
    {
        return sceneManagerLiteInstance;
    }

    IEnumerator FadeIn()
    {
        float t = 1.0f;

        while(t > 0.0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    private void SceneTransition(int [] buildIndicesToBeLoaded, int[] buildIndicesToBeUnloaded, int activeSceneIndex)
    {
        StartCoroutine(SceneTransitionRoutine(buildIndicesToBeLoaded, buildIndicesToBeUnloaded, activeSceneIndex));
    }

    private IEnumerator SceneTransitionRoutine (int[] buildIndicesToBeLoaded, int[] buildIndicesToBeUnloaded, int activeSceneIndex)
    {
        yield return StartCoroutine(FadeOut());

        foreach (int buildIndex in buildIndicesToBeLoaded)
        {
            yield return StartCoroutine(LoadScenesInOrder(buildIndex));
        }

        foreach (int buildIndex in buildIndicesToBeUnloaded)
        {
            yield return StartCoroutine(UnloadSceneRoutine(buildIndex));
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(activeSceneIndex));

        yield return StartCoroutine(FadeIn());
    }

    public void Play()
    {
        int[] scenesToBeLoaded = { levelSelectorSceneBuildIndex };
        int[] scenesToBeUnloaded = { mainMenuBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, levelSelectorSceneBuildIndex);
    }

    public void RetryLevel()
    {
        int[] scenesToBeLoaded = { currentLevelBuildIndex };
        int[] scenesToBeUnloaded = { currentLevelBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, currentLevelBuildIndex);
    }

    public void Menu()
    {
        int[] scenesToBeLoaded = { levelSelectorSceneBuildIndex };
        int[] scenesToBeUnloaded = { baseSceneBuildIndex, currentLevelBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, levelSelectorSceneBuildIndex);
    }

    public void NextLevel()
    {
        // The next level should consistently have a build index of one greater than the current level's build index.
        int[] scenesToBeLoaded = { currentLevelBuildIndex + 1 };
        int[] scenesToBeUnloaded = { currentLevelBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, currentLevelBuildIndex + 1);
    }

    public void SelectLevel(int levelSelected)
    {
        // This remains true as long as the levelSelector's build index is the one directly before all of the levels.
        // Adjust accordingly if build indices are altered.
        int levelSelectedBuildIndex = levelSelected + levelSelectorSceneBuildIndex;
        Debug.Log("levelselectedBuildIndex is: " + levelSelectedBuildIndex);

        currentLevelBuildIndex = levelSelectedBuildIndex;
        Debug.Log("currentLevelBuildIndex from SelectLevel method is: " + currentLevelBuildIndex);

        //FadeTo(levelSelectorSceneBuildIndex, levelSelectedBuildIndex);
        //LoadSceneAdditive(baseSceneBuildIndex);

        int[] scenesToBeLoaded = { baseSceneBuildIndex, currentLevelBuildIndex };
        //int[] scenesToBeLoaded = { currentLevelBuildIndex, baseSceneBuildIndex };
        int[] scenesToBeUnloaded = { levelSelectorSceneBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, currentLevelBuildIndex);
    }




    public void LoadScene(int sceneToLoadBuildIndex)
    {
        // kick-off the one co-routine to rule them all
        StartCoroutine(LoadScenesInOrder(sceneToLoadBuildIndex));
    }

    public void UnloadScene(int sceneBuildIndex)
    {
        StartCoroutine(UnloadSceneRoutine(sceneBuildIndex));
    }

    private IEnumerator LoadScenesInOrder(int sceneToLoadBuildIndex)
    {
        // LoadSceneAsync() returns an AsyncOperation, 
        // so will only continue past this point when the Operation has finished
        //yield return SceneManager.LoadSceneAsync("Loading");

        // as soon as we've finished loading the loading screen, start loading the game scene
        yield return StartCoroutine(LoadSceneRoutine(sceneToLoadBuildIndex));

        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoadBuildIndex));
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
