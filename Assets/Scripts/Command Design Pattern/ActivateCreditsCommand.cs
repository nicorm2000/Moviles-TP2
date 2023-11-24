using UnityEngine;
using CandyCoded.HapticFeedback;

public class ActivateCreditsCommand : ICommand
{
    private GameObject credits;

    public ActivateCreditsCommand(GameObject credits)
    {
        this.credits = credits;
    }

    public void Execute(AudioClip clickClip)
    {
        AudioManager.instance.PlaySound(clickClip);
        HapticFeedback.HeavyFeedback();
        credits.SetActive(true);
    }
}