using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public SkinVariant[] skinVariants;
    public int equippedSkinIndex;
}

[System.Serializable]
public class SkinVariant
{
    public string skinName;
    public Sprite skinSprite;
    public int cost;
    public bool isPurchased;
}