using UnityEngine;

public class PlayCommand : ICommand
{
    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        GameManager.instance.GoToGame();
    }
}