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

    // Fade In/Out Fields
    [Header("Fade In and Out Fields")]
    public Image img;
    public AnimationCurve curve;

    // Scene Index Variables
    private int currentLevelBuildIndex;
    [Header("Important Scene Indices to Set")]
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


    /*
     * Fade in effect that controls image alpha with Unity curve
     */
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


    /*
     * Fade out effect that controls image alpha with Unity curve
     */
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


    public int GetCurrentLevelBuildIndex()
    {
        return currentLevelBuildIndex;
    }

    public void SetCurrentLevelBuildIndex(int index)
    {
        currentLevelBuildIndex = index;
    }


    /*
     * Takes in an array of scene indices to determine which scenes to load and an array of scene indices to determine which 
     * to unload.
     * Also takes an index of which of the loaded scene to set as the new active scene.
     */
    private void SceneTransition(int [] buildIndicesToBeLoaded, int[] buildIndicesToBeUnloaded, int activeSceneIndex)
    {
        StartCoroutine(SceneTransitionRoutine(buildIndicesToBeLoaded, buildIndicesToBeUnloaded, activeSceneIndex));
    }


    /*
     * The coroutine controlling the flow of the async scene loading and unloading
     */
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


    /*
     * Controls the scenes to be loaded/unloaded when the player initially presses Play on the game opening screen
     */
    public void Play()
    {
        int[] scenesToBeLoaded = { levelSelectorSceneBuildIndex };
        int[] scenesToBeUnloaded = { mainMenuBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, levelSelectorSceneBuildIndex);
    }

    /*
     * Controls the scenes to be loaded/unloaded when the player retries a level
     */
    public void RetryLevel()
    {
        int[] scenesToBeLoaded = { currentLevelBuildIndex };
        int[] scenesToBeUnloaded = { currentLevelBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, currentLevelBuildIndex);
    }


    /*
     * Controls the scenes to be loaded/unloaded when the player goes from a level to the level select menu
     */
    public void Menu()
    {
        int[] scenesToBeLoaded = { levelSelectorSceneBuildIndex };
        int[] scenesToBeUnloaded = { baseSceneBuildIndex, currentLevelBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, levelSelectorSceneBuildIndex);
    }


    /*
     * Controls the scenes to be loaded/unloaded when the player wants to progress directly from the current level 
     * to the next level upon victory.
     */
    public void NextLevel()
    {
        // The next level should consistently have a build index of one greater than the current level's build index.
        int[] scenesToBeLoaded = { currentLevelBuildIndex + 1 };
        int[] scenesToBeUnloaded = { currentLevelBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, currentLevelBuildIndex + 1);
    }


    /*
     * Controls the scenes to be loaded/unloaded when the player selects a level to start from the level 
     * select menu.
     */
    public void SelectLevel(int levelSelected)
    {
        // This remains true as long as the levelSelector's build index is the one directly before all of the levels.
        // Adjust accordingly if build indices are altered.
        int levelSelectedBuildIndex = levelSelected + levelSelectorSceneBuildIndex;
        Debug.Log("levelselectedBuildIndex is: " + levelSelectedBuildIndex);

        currentLevelBuildIndex = levelSelectedBuildIndex;
        Debug.Log("currentLevelBuildIndex from SelectLevel method is: " + currentLevelBuildIndex);

        int[] scenesToBeLoaded = { baseSceneBuildIndex, currentLevelBuildIndex };
        int[] scenesToBeUnloaded = { levelSelectorSceneBuildIndex };

        SceneTransition(scenesToBeLoaded, scenesToBeUnloaded, currentLevelBuildIndex);
    }


    // The following methods are all from:
    // http://myriadgamesstudio.com/how-to-use-the-unity-scenemanager/
    // These are the core of running the multiple scene system with async loading
    // They have been edited slightly from their initial conditions since I am 
    // not currently using any loading screen and am using scene indices instead 
    // of scene names as inputs.

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
