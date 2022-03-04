using System;
using System.Collections.Generic;
using UnityEngine;

public class Character3rdPersonMovement : PlayerMovement
{
    [SerializeField] private CharacterController _controller;

    public class MovementData
    {
        public Vector3 Direction;
    }

    [Serializable]
    private class Keyboard
    {
        [SerializeField] private ForwardAction Up;
        [SerializeField] private BackAction Down;
        [SerializeField] private TurnAction Left;
        [SerializeField] private TurnAction Right;

        public IEnumerable<KeyAction> ForEach()
        {
            yield return Up;
            yield return Down;
            yield return Left;
            yield return Right;
        }
    }

    private abstract class KeyAction : InputKeyAction
    {
        public MovementData Movement { get; internal set; }
    }

    [Serializable]
    private class ForwardAction : KeyAction
    {
        protected override void KeyDown()
        {
            Movement.Direction += Vector3.forward;
        }

        protected override void KeyUp()
        {
            Movement.Direction -= Vector3.forward;
        }
    }

    [Serializable]
    private class BackAction : KeyAction
    {
        protected override void KeyDown()
        {
            Movement.Direction += Vector3.back;
        }

        protected override void KeyUp()
        {
            Movement.Direction -= Vector3.back;
        }
    }

    [Serializable]
    private class TurnAction : KeyAction
    {
        [SerializeField] private Vector3 _vector = Vector3.right;

        protected override void KeyDown()
        {
            Movement.Direction += _vector;
        }

        protected override void KeyUp()
        {
            Movement.Direction -= _vector;
        }
    }

    [SerializeField] private Keyboard _actions;

    private MovementData _movement = new MovementData();
    public MovementData Movement => _movement;

    public Vector3 LastPositionChange { get; protected set; }

    private MovementState state = MovementState.Stand;

    public override float CurrentSpeed => _controller.velocity.magnitude;

    public override Vector3 NormalizedVelocity => Movement.Direction; // todo

    protected override void OnStart()
    {
        base.OnStart();
        foreach (var action in _actions.ForEach())
        {
            action.Movement = _movement;
            Character.Input.KeyInteractions.Add(action);
        }
    }

    protected override void HandleInput()
    {
        if (_movement.Direction != Vector3.zero)
        {
            // todo desing state machine pattern
            if (state == MovementState.Stand)
            {
                Character.AnimatorHandler.Animator.Play("Locomotion", 0.75f);
                state = MovementState.Moving;
            }

            DisablePathfinding();

            Vector3 forward = transform.rotation * _movement.Direction;
            LastPositionChange = forward * Speed * Time.deltaTime;

            _controller.Move(LastPositionChange);

            if (!_controller.isGrounded)
            {
                _controller.Move(Vector3.down * 0.2f);
            }
        }
        else
        {
            if (state == MovementState.Moving)
            {
                Character.AnimatorHandler.Animator.Play("Idle", 0.3f);
                state = MovementState.Stand;
            }
        }
    }

    public override void Stop()
    {
        LastPositionChange = Vector3.zero;
    }

    public override void Wrap(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public override void Move(Vector3 move)
    {
        _controller.Move(move);
    }

    protected override void OnSpeedChange()
    {
    }

    public override void SetDestination(Transform transform)
    {
        EnablePathfinding();
        base.SetDestination(transform);
        _movement.Direction = Vector3.zero;
    }
}
