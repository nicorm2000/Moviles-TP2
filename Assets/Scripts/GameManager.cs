using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isInitialized { get; set; }
    public int equippedSkinIndex { get; set; }
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
    public PlayerData playerData;

    private const string _mainMenu = "MainMenu";
    private const string _game = "Game";
    private const string _shop = "Shop";
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
        equippedSkinIndex = PlayerPrefs.GetInt("EquippedSkinIndex", 0);
    }

    public void GoToMainMenu()
    {
        LoadingManager.Instance.LoadScene(_mainMenu);
    }

    public void GoToGame()
    {
        LoadingManager.Instance.LoadScene(_game);
    }

    public void GoToShop()
    {
        LoadingManager.Instance.LoadScene(_shop);
    }
}