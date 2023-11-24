using UnityEngine;
using CandyCoded.HapticFeedback;
public class MainMenuCommand : ICommand
{
    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.HeavyFeedback();
        GameManager.instance.GoToMainMenu();
    }
}