using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TMP_InputField inputField;
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

    public void Write()
    {
        if (inputField.text != string.Empty)
        {
            FileManager.WriteFile(inputField.text + "\n");
        }
    }

    public void Delete()
    {
        FileManager.DeleteFile();
    }

    public void ShowAchievementsUI()
    {
        Debug.Log("Show Achievements");
        PlayGamesManager.ShowAchievementsUI();
    }

    public void ClickedPlay()
    {
        Debug.Log("Play Game");
        _playCommand.Execute(clickClip);
    }

    public void ClickedShop()
    {
        Debug.Log("Go to Shop");
        _shopCommand.Execute(clickClip);
    }

    public void ActivateCredits()
    {
        Debug.Log("Credits ON");
        _activateCreditsCommand.Execute(clickClip);
    }

    public void DeactivateCredits()
    {
        Debug.Log("Credits OFF");
        _deactivateCreditstCommand.Execute(clickClip);
    }

    public void ActivatePlugin()
    {
        Debug.Log("Plugin ON");
        _activatePluginCommand.Execute(clickClip);
    }

    public void DeactivatePlugin() 
    {
        Debug.Log("Plugin OFF");
        _deactivatePluginCommand.Execute(clickClip);
    }

    public void OpenURL(string url)
    {
        Debug.Log("Open Link");
        _openURLCommand = new OpenURLCommand(url);
        _openURLCommand.Execute(clickClip);
    }
}