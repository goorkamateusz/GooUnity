using System;
using Assets.Goo.Characters;
using UnityEngine;

/// idea No editor state machine animation management
public class AnimatorHandler : CharacterComponent
{
    [SerializeField] protected AnimatorAsyncController _animator;

    // todo good practice?
    public AnimatorAsyncController Animator => _animator;

    [Obsolete]
    public void SetTrigger(string name)
    {
        _animator.Animator.SetTrigger(name);
    }

    [Obsolete]
    public void SetInt(string name, int value)
    {
        _animator.Animator.SetInteger(name, value);
    }

    [Obsolete]
    public void SetBool(string name, bool value = true)
    {
        _animator.Animator.SetBool(name, value);
    }

    protected virtual void Update()
    {
        // todo temporary
        if (Input.GetKeyDown(KeyCode.W))
        {
            const string StateName = "Locomotion";
            const float FixedTransitionDuration = 0.75f;
            _animator.Play(StateName, FixedTransitionDuration);
        }
    }
}
