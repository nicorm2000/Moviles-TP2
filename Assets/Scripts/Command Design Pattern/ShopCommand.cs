using UnityEngine;

public class ShopCommand : ICommand
{
    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        GameManager.instance.GoToShop();
    }
}