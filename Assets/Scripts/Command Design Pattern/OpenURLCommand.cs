using UnityEngine;
using CandyCoded.HapticFeedback;

public class OpenURLCommand : ICommand
{
    private string url;

    public OpenURLCommand(string url)
    {
        this.url = url;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.MediumFeedback();
        Application.OpenURL(url);
    }
}