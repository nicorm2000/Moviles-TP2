using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using TMPro;
using System.Collections;

public class PlayGamesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI detailsText;

    private float changeScene = 2f;

    private void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgURL = PlayGamesPlatform.Instance.GetUserImageUrl();

            detailsText.text = "Success \n" + name;

            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            detailsText.text = "Sign In failed!";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            StartCoroutine(NextSceneTimer());
        }
    }

    private IEnumerator NextSceneTimer()
    {
        yield return new WaitForSeconds(changeScene);

        SceneManager.LoadScene("MainMenu");
    }
}