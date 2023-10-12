using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isInitialized { get; set; }
    public int currentScore { get; set; }
    public int highScore 
    {
        get 
        {
            return PlayerPrefs.GetInt(_highScoreKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(_highScoreKey, value);
        }
    }

    private string _highScoreKey = "HighScore";

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        currentScore = 0;
        isInitialized = false;
    }

    private const string _mainMenu = "MainMenu"; 
    private const string _game = "Game"; 

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(_mainMenu);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(_game);
    }
}