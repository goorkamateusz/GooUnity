using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class AnimatorAsyncController
{
    [SerializeField] private Animator animator;

    private string currentState;

    public Animator Animator => animator;

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
