using UnityEngine;
using CandyCoded.HapticFeedback;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    public void ReturnToMainMenu()
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.MediumFeedback();
        GameManager.instance.GoToMainMenu();
    }
}