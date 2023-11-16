using UnityEngine;
using CandyCoded.HapticFeedback;

public class DeactivateObjectCommand : ICommand
{
    private GameObject credits;

    public DeactivateObjectCommand(GameObject credits)
    {
        this.credits = credits;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.MediumFeedback();
        credits.SetActive(false);
    }
}