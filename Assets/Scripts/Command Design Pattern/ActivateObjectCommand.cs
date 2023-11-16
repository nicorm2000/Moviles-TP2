using UnityEngine;
using CandyCoded.HapticFeedback;

public class ActivateObjectCommand : ICommand
{
    private GameObject credits;

    public ActivateObjectCommand(GameObject credits)
    {
        this.credits = credits;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.MediumFeedback();
        credits.SetActive(true);
    }
}