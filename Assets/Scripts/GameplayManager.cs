using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialIcon;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private GameObject tutorialSemiCircle;
    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private List<int> levelSpeed, levelMax;
    [SerializeField] private float tutorialSemiCircleTime;
    [SerializeField] private float gameOverTime;
    [SerializeField] private AudioClip deathClip;

    public static bool hasStarted = false;

    private bool _hasGameFinished;
    private float _score;
    private float _scoreSpeed;
    private int _currentLevel;

    #region ACHIEVEMENTS
    private bool _unlockedAchievement1 = false;
    private bool _unlockedAchievement2 = false;
    private bool _unlockedAchievement3 = false;
    private bool _unlockedAchievement4 = false;

    private const int _scoreAchievement1 = 100;
    private const int _scoreAchievement2 = 1000;
    private const int _scoreAchievement3 = 5000;
    private const int _scoreAchievement4 = 10000;
    #endregion

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
            StartCoroutine(HideTutorialSemiCircle());
            Debug.Log("Hide Tutorial");
            _score += _scoreSpeed * Time.deltaTime;
            score_Text.text = ((int)_score).ToString();

            if (Social.localUser.authenticated)
            {
                //Achievement 100 score
                if (_score >= _scoreAchievement1 && !_unlockedAchievement1)
                {
                    PlayGamesManager.UnlockAchievemt(GPGSIds.achievement_100_club);
                    _unlockedAchievement1 = true;
                    Debug.Log("Unlocked Achievement 1");
                }

                //Achievement 1000 score
                if (_score >= _scoreAchievement2 && !_unlockedAchievement2)
                {
                    PlayGamesManager.UnlockAchievemt(GPGSIds.achievement_100_club);
                    _unlockedAchievement2 = true;
                    Debug.Log("Unlocked Achievement 2");
                }

                //Achievement 5000 score
                if (_score >= _scoreAchievement3 && !_unlockedAchievement3)
                {
                    PlayGamesManager.UnlockAchievemt(GPGSIds.achievement_100_club);
                    _unlockedAchievement3 = true;
                    Debug.Log("Unlocked Achievement 3");
                }

                //Achievement 10000 score
                if (_score >= _scoreAchievement4 && !_unlockedAchievement4)
                {
                    PlayGamesManager.UnlockAchievemt(GPGSIds.achievement_100_club);
                    _unlockedAchievement4 = true;
                    Debug.Log("Unlocked Achievement 4");
                }
            }

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
        Debug.Log("Game Ended");
        AudioManager.instance.PlaySound(deathClip);
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

    private IEnumerator HideTutorialSemiCircle()
    {
        TextMeshProUGUI textMeshPro = tutorialSemiCircle.GetComponent<TextMeshProUGUI>();

        float elapsedTime = 0f;

        Color textColor = textMeshPro.color;

        while (elapsedTime < tutorialSemiCircleTime)
        {
            float remainingTime = tutorialSemiCircleTime - elapsedTime;
            float alpha = remainingTime / tutorialSemiCircleTime;

            textColor.a = alpha;
            textMeshPro.color = textColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tutorialSemiCircle.SetActive(false);
    }
}