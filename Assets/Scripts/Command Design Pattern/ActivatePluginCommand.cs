using UnityEngine;
using CandyCoded.HapticFeedback;

public class ActivatePluginCommand : ICommand
{
    private GameObject plugin;

    public ActivatePluginCommand(GameObject plugin)
    {
        this.plugin = plugin;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.MediumFeedback();
        plugin.SetActive(true);
    }
}