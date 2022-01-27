using System;
using System.Collections.Generic;
using UnityEngine;

public class Character3rdPersonMovement : CharacterMovement
{
    public class MovementData
    {
        public Vector3 Direction;
    }

    [Serializable]
    private class Keyboard<T> where T : new()
    {
        public T Up = new T();
        public T Down = new T();
        public T Left = new T();
        public T Right = new T();

        public IEnumerable<T> ForEach()
        {
            yield return Up;
            yield return Down;
            yield return Left;
            yield return Right;
        }
    }

    [Serializable]
    private class KeyAction : InputKeyAction
    {
        public Vector3 Vector;
        public MovementData Movement { get; internal set; }

        protected override void KeyDown()
        {
            Movement.Direction += Vector;
        }

        protected override void KeyUp()
        {
            Movement.Direction -= Vector;
        }
    }

    [SerializeField] private Keyboard<KeyAction> _actions;

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
        _agent.SetDestination(transform.position + (Movement.Direction * Speed));
    }
}
