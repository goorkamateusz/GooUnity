using System;
using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(Collider))]
// [RequireComponent(typeof(NavMeshAgent))]
public abstract class CharacterMovement : CharacterComponent
{
    [SerializeField] protected NavMeshAgent _agent; // todo it should be in other class
    [SerializeField] private float _speedOriginal = 5f;

    private MovementTaskProvider _tasks = new MovementTaskProvider();
    private float _speedMultiplier = 1f;

    public bool IsEnabled { get; set; } = true;

    public NavMeshAgent Agent => _agent;
    public float Speed => _speedOriginal * _speedMultiplier;
    public float OriginalSpeed => _speedOriginal;
    public abstract float CurrentSpeed { get; }

    public MovementTaskProvider Tasks => _tasks;

    public void SetMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
        OnSpeedChange();
    }

    public void ResetMultiplier()
    {
        _speedMultiplier = 1f;
        OnSpeedChange();
    }

    public abstract void Stop();

    public virtual void Wrap(Transform target)
    {
        Wrap(target.position, target.rotation);
    }

    public abstract void Wrap(Vector3 position, Quaternion rotation);

    protected abstract void HandleInput();

    protected virtual void Awake()
    {
        OnSpeedChange();
    }

    protected virtual void Update()
    {
        if (IsEnabled)
        {
            HandleInput();
        }

        if (Tasks.CheckAll())
        {
            Stop();
        }
    }

    protected abstract void OnSpeedChange();

    public virtual void SetDestination(Transform target)
    {
        // todo pathfinding in navmashagant but moving realise with char. controller
        _agent.Warp(transform.position);
        _agent.SetDestination(target.position);
    }

    public void EnablePathfinding()
    {
        _agent.updatePosition = _agent.updateRotation = true;
    }

    public void DisablePathfinding()
    {
        _agent.updatePosition = _agent.updateRotation = false;
    }
}
