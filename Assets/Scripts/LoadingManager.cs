using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }

    const string loadingScene = "LoadingScene";
    const float waitTimer = 1f;
    const float activationProgress = 0.9f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading Scene");
        StartCoroutine(InternalLoadScene(sceneName));
    }

    private IEnumerator InternalLoadScene(string sceneName)
    {
        SceneManager.LoadScene(loadingScene);
        yield return null;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= activationProgress)
                asyncLoad.allowSceneActivation = true;

            yield return new WaitForSeconds(waitTimer);
        }

        yield return new WaitForSeconds(waitTimer);

        asyncLoad.allowSceneActivation = true;
        Debug.Log("Scene Loaded");
    }
}