using UnityEngine;
using CandyCoded.HapticFeedback;

public class DeactivatePluginCommand : ICommand
{
    private GameObject plugin;

    public DeactivatePluginCommand(GameObject plugin)
    {
        this.plugin = plugin;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.HeavyFeedback();
        plugin.SetActive(false);
    }
}