using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip error;
    [SerializeField] private AudioClip buy;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image playerPreviewImage;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text skinValue;
    [SerializeField] private TMP_Text skinName;
    [SerializeField] private Button purchase;
    [SerializeField] private Button equip;
    [SerializeField] private float canNotBuySkin;

    private int currentSkinIndex;
    private ICommand _mainMenuCommand = new MainMenuCommand();

    #region ACHIEVEMENTS
    private bool _unlockedAchievement5 = false;
    private bool _firstPurchase = false;
    #endregion

    private void Start()
    {
        currentSkinIndex = playerData.equippedSkinIndex;
        UpdateUI();
    }

    public void PurchaseSkin()
    {
        int currentCoins = PlayerPrefs.GetInt("Coins", 0);
        int skinCost = GameManager.instance.playerData.skinVariants[currentSkinIndex].cost;

        if (currentCoins >= skinCost)
        {
            AudioManager.instance.PlaySound(buy);
            currentCoins -= skinCost;
            PlayerPrefs.SetInt("Coins", currentCoins);

            GameManager.instance.playerData.skinVariants[currentSkinIndex].isPurchased = true;
            GameManager.instance.playerData.equippedSkinIndex = currentSkinIndex;

            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                //Achievement Make first purchase in the store
                if (!_unlockedAchievement5 && !_firstPurchase)
                {
                    PlayGamesManager.UnlockAchievemt(GPGSIds.achievement_big_money);
                    _unlockedAchievement5 = true;
                    _firstPurchase = true;
                    Debug.Log("Unlocked Achievement 5");
                }
            }
            UpdateUI();
        }
        else
        {
            FileManager.WriteFile("Not enough coins to purchase the skin.");
            Debug.Log("Not enough coins to purchase the skin.");
            AudioManager.instance.PlaySound(error);
        }
    }

    public void EquipSkin()
    {
        AudioManager.instance.PlaySound(clickClip);
        GameManager.instance.playerData.equippedSkinIndex = currentSkinIndex;
        UpdateUI();
        Debug.Log("Skin equipped: " + GameManager.instance.playerData.skinVariants[currentSkinIndex].skinName);
    }

    public void NextSkin()
    {
        if (currentSkinIndex < GameManager.instance.playerData.skinVariants.Length - 1)
        {
            currentSkinIndex++;
            UpdateUI();
        }
    }

    public void PreviousSkin()
    {
        if (currentSkinIndex > 0)
        {
            currentSkinIndex--;
            UpdateUI();
        }
    }

    public void OnAddCoinsButtonClicked()
    {
        AddCoins(10);
    }

    private void AddCoins(int amount)
    {
        int currentCoins = PlayerPrefs.GetInt("Coins", 0);
        currentCoins += amount;
        PlayerPrefs.SetInt("Coins", currentCoins);
        UpdateUI();
    }

    private void UpdateUI()
    {
        SkinVariant currentSkin = GameManager.instance.playerData.skinVariants[currentSkinIndex];

        playerPreviewImage.sprite = currentSkin.skinSprite;
        skinName.text = GameManager.instance.playerData.skinVariants[currentSkinIndex].skinName;
        skinValue.text = GameManager.instance.playerData.skinVariants[currentSkinIndex].cost.ToString();
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();

        purchase.interactable = !currentSkin.isPurchased;
        equip.interactable = currentSkin.isPurchased;
    }

    public void ClickedMainMenu()
    {
        _mainMenuCommand.Execute(clickClip);
    }
}