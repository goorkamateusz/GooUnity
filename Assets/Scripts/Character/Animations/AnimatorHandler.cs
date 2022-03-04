using System;
using Assets.Goo.Characters;
using Assets.Goo.Characters.Animations;
using UnityEngine;

public class AnimatorHandler : CharacterComponent
{
    /// idea No editor state machine animation management
    /// idea translate enum to string state names
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
}
