using System;
using System.Collections.Generic;
using UnityEngine;

public class Character3rdPersonMovement : CharacterMovement
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

    private MovementData Movement = new MovementData();

    public Vector3 LastPositionChange { get; protected set; }

    public override float CurrentSpeed => _controller.velocity.magnitude;

    protected override void OnStart()
    {
        base.OnStart();
        foreach (var action in _actions.ForEach())
        {
            action.Movement = Movement;
            Character.Input.KeyInteractions.Add(action);
        }
    }

    protected override void HandleInput()
    {
        if (Movement.Direction != Vector3.zero)
        {
            DisablePathfinding();
            
            Vector3 forward = transform.rotation * Movement.Direction;
            LastPositionChange = forward * Speed * Time.deltaTime;

            _controller.Move(LastPositionChange);

            if (!_controller.isGrounded)
            {
                _controller.Move(Vector3.down * 0.2f);
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
        Movement.Direction = Vector3.zero;
    }
}
