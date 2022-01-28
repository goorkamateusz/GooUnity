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

    public Vector3 Move { get; protected set; }

    protected override void OnStart()
    {
        foreach (var action in _actions.ForEach())
        {
            action.Movement = Movement;
            Character.Input.KeyInteractions.Add(action);
        }
    }

    protected override void HandleInput()
    {
    }

    protected override void Update()
    {
        base.Update();

        Vector3 forward = transform.rotation * Movement.Direction;
        Move = forward * Speed * Time.deltaTime;

        _controller.Move(Move);

        if (!_controller.isGrounded)
        {
            _controller.Move(Vector3.down * 0.2f);
        }
    }
}
