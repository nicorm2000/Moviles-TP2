using UnityEngine;

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
        Application.OpenURL(url);
    }
}