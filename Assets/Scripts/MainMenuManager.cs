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
    [SerializeField] private TMPro.TMP_InputField inputField;

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
        FileManager.ReadFile();
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

    public void ClickedPlay()
    {
        _playCommand.Execute(clickClip);
    }

    public void ClickedShop()
    {
        _shopCommand.Execute(clickClip);
    }

    public void ActivateCredits()
    {
        _activateCreditsCommand.Execute(clickClip);
    }

    public void DeactivateCredits()
    {
        _deactivateCreditstCommand.Execute(clickClip);
    }

    public void ActivatePlugin()
    {
        _activatePluginCommand.Execute(clickClip);
    }

    public void DeactivatePlugin() 
    {
        _deactivatePluginCommand.Execute(clickClip);
    }

    public void OpenURL(string url)
    {
        _openURLCommand = new OpenURLCommand(url);
        _openURLCommand.Execute(clickClip);
    }
}