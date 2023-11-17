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
        // Ensure there is only one instance of the LoadingScreen
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
        Debug.Log("Before wait timer");
        yield return new WaitForSeconds(waitTimer);
        Debug.Log("After wait timer");
        asyncLoad.allowSceneActivation = true;
    }
}