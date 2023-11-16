using UnityEngine;
using CandyCoded.HapticFeedback;

public class ShopCommand : ICommand
{
    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.MediumFeedback();
        GameManager.instance.GoToShop();
    }
}