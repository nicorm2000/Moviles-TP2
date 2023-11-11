using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable/CreateShopData")]
public class ShopSave : ScriptableObject
{
    public ShopItem[] shopItems;
}

[System.Serializable]
public class ShopItem
{ 
    public string itemName;
    public bool isUnlocked;
    public int unlocckCost;
    public int unlockedLevel;
    public SkinInfo[] skinLevel;
}

[System.Serializable]
public class SkinInfo
{
    public int unlockCost;
    public Color playerColor;
    public Color enemyColor;
    public Color circleColor;
    public Color bgColor;
}