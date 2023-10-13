using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text newBestText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve speedCurve;

    private void Awake()
    {
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
        AudioManager.instance.PlaySound(clickClip);
        GameManager.instance.GoToGame();
    }
}