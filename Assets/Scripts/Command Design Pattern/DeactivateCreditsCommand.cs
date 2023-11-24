using UnityEngine;
using CandyCoded.HapticFeedback;

public class DeactivateCreditsCommand : ICommand
{
    private GameObject credits;

    public DeactivateCreditsCommand(GameObject credits)
    {
        this.credits = credits;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.HeavyFeedback();
        credits.SetActive(false);
    }
}