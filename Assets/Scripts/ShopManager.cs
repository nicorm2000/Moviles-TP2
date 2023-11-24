using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image playerPreviewImage;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text skinValue;
    [SerializeField] private TMP_Text skinName;
    [SerializeField] private TextMeshProUGUI unlockSkinVisualButton;
    [SerializeField] private Color unlockSkinVisualButtonActive;
    [SerializeField] private Color unlockSkinVisualButtonInactive;
    [SerializeField] private float canNotBuySkin;

    private int currentSkinIndex;
    private ICommand _mainMenuCommand = new MainMenuCommand();

    private void Start()
    {
        currentSkinIndex = playerData.equippedSkinIndex;
        UpdateUI();
    }

    public void PurchaseSkin()
    {
        if (currentSkinIndex < playerData.skinVariants.Length - 1)
        {
            int currentCoins = PlayerPrefs.GetInt("Coins", 0);
            int skinCost = playerData.skinVariants[currentSkinIndex + 1].cost;

            if (currentCoins >= skinCost)
            {
                currentCoins -= skinCost;
                PlayerPrefs.SetInt("Coins", currentCoins);

                currentSkinIndex++;
                playerData.equippedSkinIndex = currentSkinIndex;
                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough coins to purchase the skin.");
                StartCoroutine(SkinTooExpensive());
            }
        }
        else
        {
            Debug.Log("All skins are already purchased.");
        }
    }

    public void EquipSkin()
    {
        playerData.equippedSkinIndex = currentSkinIndex;
        UpdateUI();
    }

    public void NextSkin()
    {
        if (currentSkinIndex < playerData.skinVariants.Length - 1)
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

    private void UpdateUI()
    {
        playerPreviewImage.sprite = playerData.skinVariants[currentSkinIndex].skinSprite;
        skinName.text = playerData.skinVariants[currentSkinIndex].skinName;
        skinValue.text = playerData.skinVariants[currentSkinIndex].cost.ToString();
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    private IEnumerator SkinTooExpensive()
    {
        unlockSkinVisualButton = GetComponent<TextMeshProUGUI>();
        unlockSkinVisualButton.color = unlockSkinVisualButtonInactive;
        yield return new WaitForSeconds(canNotBuySkin);
        unlockSkinVisualButton.color = unlockSkinVisualButtonActive;
    }

    public void ClickedMainMenu()
    {
        _mainMenuCommand.Execute(clickClip);
    }
}