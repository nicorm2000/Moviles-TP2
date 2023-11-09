using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialIcon;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private List<int> levelSpeed, levelMax;

    public bool hasStarted = false;

    private bool _hasGameFinished;
    private float _score;
    private float _scoreSpeed;
    private int _currentLevel;

    private void Awake()
    {
        GameManager.instance.isInitialized = true;

        _hasGameFinished = false;
        _score = 0;
        _currentLevel = 0;
        score_Text.text = ((int)_score).ToString();
        _scoreSpeed = levelSpeed[_currentLevel];
    }

    private void Update()
    {
        if (_hasGameFinished)
        {
            return;
        }

        if (hasStarted)
        {
            HideTutorial();
            _score += _scoreSpeed * Time.deltaTime;
            score_Text.text = ((int)_score).ToString();

            if (_score > levelMax[Mathf.Clamp(_currentLevel, 0, levelSpeed.Count - 1)])
            {
                _currentLevel = Mathf.Clamp(_currentLevel + 1, 0, levelSpeed.Count - 1);
                _scoreSpeed = levelSpeed[_currentLevel];
            }
        }
    }

    public void GameEnded()
    { 
        _hasGameFinished = true;
        hasStarted = false;
        GameManager.instance.currentScore = (int)_score;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.GoToMainMenu();
    }

    private void HideTutorial() 
    {
        tutorialIcon.SetActive(false);
        tutorialText.SetActive(false);
    }
}