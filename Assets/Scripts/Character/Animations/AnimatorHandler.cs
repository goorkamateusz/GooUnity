using System;
using UnityEngine;

/// idea No editor state machine animation management
public class AnimatorHandler : CharacterComponent
{
    [SerializeField] private Animator _animator;

    private AnimatorAsyncController _stateController;

    [Obsolete]
    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    [Obsolete]
    public void SetInt(string name, int value)
    {
        _animator.SetInteger(name, value);
    }

    [Obsolete]
    public void SetBool(string name, bool value = true)
    {
        _animator.SetBool(name, value);
    }

    protected override void OnStart()
    {
        _stateController = new AnimatorAsyncController(_animator);
    }

    protected virtual void Update()
    {
        // todo temporary
        if (Input.GetKeyDown(KeyCode.W))
        {
            const string StateName = "Walk";
            const float FixedTransitionDuration = 0.75f;
            _stateController.Play(StateName, FixedTransitionDuration);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            const string StateName = "Idle";
            _stateController.Play(StateName, 0.5f);
        }
    }
}
