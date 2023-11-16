using UnityEngine;
using TMPro;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text newBestText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve speedCurve;

    private ICommand playCommand = new PlayCommand();
    private ICommand shopCommand = new ShopCommand();
    private ICommand activateObjectCommand;
    private ICommand deactivateObjectCommand;
    private ICommand openURLCommand;

    private void Awake()
    {
        activateObjectCommand = new ActivateObjectCommand(credits);
        deactivateObjectCommand = new DeactivateObjectCommand(credits);

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

    public void ClickedPlay()
    {
        playCommand.Execute(clickClip);
    }

    public void ClickedShop()
    {
        shopCommand.Execute(clickClip);
    }

    public void ActivateObject()
    {
        activateObjectCommand.Execute(clickClip);
    }

    public void DeactivateObject()
    {
        deactivateObjectCommand.Execute(clickClip);
    }

    public void OpenURL(string url)
    {
        openURLCommand = new OpenURLCommand(url);
        openURLCommand.Execute(clickClip);
    }
}