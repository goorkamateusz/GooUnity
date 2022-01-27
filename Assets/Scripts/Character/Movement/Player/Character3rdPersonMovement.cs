using System;
using System.Collections.Generic;
using UnityEngine;

public class Character3rdPersonMovement : CharacterMovement
{
    [SerializeField] private CharacterController _controller;

    public class MovementData
    {
        public int Sign;
        public float Angle;
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
            Movement.Sign = 1;
        }

        protected override void KeyUp()
        {
            Movement.Sign = 0;
        }
    }

    [Serializable]
    private class BackAction : KeyAction
    {
        protected override void KeyDown()
        {
            Movement.Sign = -1;
        }

        protected override void KeyUp()
        {
            Movement.Sign = 0;
        }
    }

    [Serializable]
    private class TurnAction : KeyAction
    {
        [SerializeField] private float _angleSpeed = 180;

        protected override void KeyDown()
        {
            Movement.Angle = _angleSpeed;
        }

        protected override void KeyUp()
        {
            Movement.Angle = 0;
        }
    }

    [SerializeField] private Keyboard _actions;

    private MovementData Movement = new MovementData();

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

        Quaternion rotation = Quaternion.AngleAxis(Movement.Angle * Time.deltaTime, Vector3.up);
        Vector3 forward = rotation * transform.forward;
        Vector3 move = Movement.Sign * forward * Speed * Time.deltaTime;

        transform.localRotation *= rotation;
        _controller.Move(move);

        if (!_controller.isGrounded)
        {
            _controller.Move(Vector3.down * 0.2f);
        }
    }
}
