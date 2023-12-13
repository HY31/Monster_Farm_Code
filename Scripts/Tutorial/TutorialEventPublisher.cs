using System;

// Publisher
public class TutorialEventPublisher
{
    public event Action<int> OnTutorialStepCompleted;


    public void PublishTutorialStep(int step)
    {
        OnTutorialStepCompleted?.Invoke(step);
    }
}
