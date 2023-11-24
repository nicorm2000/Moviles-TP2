using UnityEngine;
using CandyCoded.HapticFeedback;

public class PlayCommand : ICommand
{
    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.HeavyFeedback();
        GameManager.instance.GoToGame();
    }
}