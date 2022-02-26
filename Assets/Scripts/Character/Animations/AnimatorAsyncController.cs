using System.Threading.Tasks;
using UnityEngine;

/// idea No editor state machine animation management
public class AnimatorAsyncController
{
    private Animator animator;
    public string currentState;

    public Animator Animator => animator;

    public AnimatorAsyncController(Animator animator)
    {
        this.animator = animator;
    }

    public async void Play(string stateName, float fixedTransitionDuration = 0f)
    {
        if (fixedTransitionDuration > 0)
        {
            await PlayWithTransition(stateName, fixedTransitionDuration);
        }
        else
        {
            currentState = stateName;
            animator.Play(stateName);
        }
    }

    private async Task PlayWithTransition(string stateName, float fixedTransitionDuration)
    {
        currentState = stateName;

        animator.CrossFadeInFixedTime(stateName, fixedTransitionDuration);
        await Task.Delay((int)(fixedTransitionDuration * 1000));

        if (currentState == stateName)
            animator.Play(stateName);
    }
}
