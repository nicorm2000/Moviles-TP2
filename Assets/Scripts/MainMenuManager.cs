using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;

    private void Awake()
    {
        if (GameManager.instance.isInitialized)
        {
            StartCoroutine(ShowScore());
        }
        else
        {
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);
            _highScoreText.text = GameManager.instance.highScore.ToString();
        }
    }

    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager.instance.currentScore;
        int highScore = GameManager.instance.highScore;

        if (highScore < currentScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager.instance.highScore = currentScore;
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
        }

        _highScoreText.text = GameManager.instance.highScore.ToString();

        float speed = 1 / _animationTime;
        float timeElapsed = 0f;

        while (timeElapsed < _animationTime)
        {
            timeElapsed += speed * Time.deltaTime;

            tempScore = (int)(_speedCurve.Evaluate(timeElapsed) * currentScore);
            _scoreText.text = tempScore.ToString();
            
            yield return null;
        }

        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
    }

    public void ClickedPlay()
    {
        AudioManager.instance.PlaySound(_clickClip);
        GameManager.instance.GoToGame();
    }
}