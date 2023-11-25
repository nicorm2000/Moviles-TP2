using System.Collections;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject pluginViewer;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text newBestText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private TMP_Text inputText;
    [SerializeField] private TMP_Text coinsText;

    private ICommand _playCommand = new PlayCommand();
    private ICommand _shopCommand = new ShopCommand();
    private ICommand _activateCreditsCommand;
    private ICommand _deactivateCreditstCommand;
    private ICommand _activatePluginCommand;
    private ICommand _deactivatePluginCommand;
    private ICommand _openURLCommand;

    private void Awake()
    {
        _activateCreditsCommand = new ActivateCreditsCommand(credits);
        _deactivateCreditstCommand = new DeactivateCreditsCommand(credits);
        _activatePluginCommand = new ActivatePluginCommand(pluginViewer);
        _deactivatePluginCommand = new DeactivatePluginCommand(pluginViewer);

        if (GameManager.instance.isInitialized)
        {
            StartCoroutine(ShowScore());
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            newBestText.gameObject.SetActive(false);
            highScoreText.text = GameManager.instance.highScore.ToString();
        }
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        scoreText.text = tempScore.ToString();

        int currentScore = GameManager.instance.currentScore;
        int highScore = GameManager.instance.highScore;

        if (highScore < currentScore)
        {
            newBestText.gameObject.SetActive(true);
            GameManager.instance.highScore = currentScore;
        }
        else
        {
            newBestText.gameObject.SetActive(false);
        }

        highScoreText.text = GameManager.instance.highScore.ToString();

        float speed = 1 / animationTime;
        float timeElapsed = 0f;

        while (timeElapsed < animationTime)
        {
            timeElapsed += speed * Time.deltaTime;

            tempScore = (int)(speedCurve.Evaluate(timeElapsed) * currentScore);
            scoreText.text = tempScore.ToString();
            
            yield return null;
        }

        tempScore = currentScore;
        scoreText.text = tempScore.ToString();
    }

    public void Read()
    {
        inputText.text = FileManager.ReadFile();
    }

    public void Delete()
    {
        FileManager.DeleteFile();
        inputText.text = FileManager.ReadFile();
    }

    public void ShowAchievementsUI()
    {
        FileManager.WriteFile("Show Achievements");
        Debug.Log("Show Achievements");
        PlayGamesManager.ShowAchievementsUI();
    }

    public void ClickedPlay()
    {
        FileManager.WriteFile("Play Game");
        Debug.Log("Play Game");
        _playCommand.Execute(clickClip);
    }

    public void ClickedShop()
    {
        FileManager.WriteFile("Go to Shop");
        Debug.Log("Go to Shop");
        _shopCommand.Execute(clickClip);
    }

    public void ActivateCredits()
    {
        FileManager.WriteFile("Credits Open");
        Debug.Log("Credits Open");
        _activateCreditsCommand.Execute(clickClip);
    }

    public void DeactivateCredits()
    {
        FileManager.WriteFile("Credits Close");
        Debug.Log("Credits Close");
        _deactivateCreditstCommand.Execute(clickClip);
    }

    public void ActivatePlugin()
    {
        FileManager.WriteFile("Plugin Open");
        Debug.Log("Plugin Open");
        _activatePluginCommand.Execute(clickClip);
    }

    public void DeactivatePlugin() 
    {
        FileManager.WriteFile("Plugin Close");
        Debug.Log("Plugin Close");
        _deactivatePluginCommand.Execute(clickClip);
    }

    public void OpenURL(string url)
    {
        FileManager.WriteFile("Open Link");
        Debug.Log("Open Link");
        _openURLCommand = new OpenURLCommand(url);
        _openURLCommand.Execute(clickClip);
    }
}