using System;
using UnityEngine;

[Serializable]
public class AnimatorHandler
{
    [SerializeField] private Animator _animator;

    public void SetMoveSpeed(float moveSpeed)
    {
        _animator.SetFloat("Speed", moveSpeed);
    }
}