using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Goo.Characters.Animations
{
    [Serializable]
    public class AnimatorAsyncController
    {
        [SerializeField] private Animator animator;

        private string currentState;

        public Animator Animator => animator;

        public async void Play(string stateName, float fixedTransitionDuration = 0f, int layer = 0)
        {
            if (fixedTransitionDuration > 0)
            {
                await PlayWithTransition(stateName, fixedTransitionDuration, layer);
            }
            else
            {
                currentState = stateName;
                animator.Play(stateName, layer);
            }
        }

        private async Task PlayWithTransition(string stateName, float fixedTransitionDuration, int layer)
        {
            currentState = stateName;

            animator.CrossFadeInFixedTime(stateName, fixedTransitionDuration, layer);
            await Task.Delay((int)(fixedTransitionDuration * 1000));

            if (currentState == stateName)
                animator.Play(stateName, layer);
        }
    }
}