using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using TMPro;

public class PlayGamesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI detailsText;
    public static bool signedIn;

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

            signedIn = true;

            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            detailsText.text = "Sign In failed!";
        }
    }

    public void LoginFailed()
    {
        signedIn = false;
        SceneManager.LoadScene("MainMenu");
    }

    #region ACHIEVEMENTS
    public static void UnlockAchievemt(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion
}