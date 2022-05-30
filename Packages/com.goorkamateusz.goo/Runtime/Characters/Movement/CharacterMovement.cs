using System;
using Assets.Goo.Characters;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(Collider))]
// [RequireComponent(typeof(NavMeshAgent))]
public abstract class CharacterMovement<T> : CharacterComponent<T> where T : Character
{
    // wip
    // todo movement status
    // todo Speed object with temporary multiplers
    [SerializeField] protected CharacterPathfinding _pathfinding;
    [SerializeField] private float _speedOriginal = 5f;

    private MovementTaskProvider _tasks = new MovementTaskProvider();
    private float _speedMultiplier = 1f;

    public bool IsEnabled { get; set; } = true;

    public float Speed => _speedOriginal * _speedMultiplier;
    public float OriginalSpeed => _speedOriginal;
    public abstract float CurrentSpeed { get; }

    public MovementTaskProvider Tasks => _tasks;

    public CharacterPathfinding Pathfinding => _pathfinding;


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

    protected override void OnStart()
    {
        _pathfinding.InjectCharacter(Character);
    }

    protected virtual void Update()
    {
        if (IsEnabled)
        {
            HandleInput();
        }

        if (_pathfinding.Active)
        {
            _pathfinding.Update();
        }

        if (Tasks.CheckAll())
        {
            Stop();
        }
    }

    public abstract void Move(Vector3 move);

    protected abstract void OnSpeedChange();

    public virtual void SetDestination(Transform target) => _pathfinding.SetDestination(target.position);

    public void EnablePathfinding() => _pathfinding.Enable();

    public void DisablePathfinding() => _pathfinding.Disable();
}

public abstract class CharacterMovement : CharacterMovement<Character>
{
    public bool State { get; internal set; }
    public virtual Vector3 NormalizedVelocity { get; internal set; }
}

public abstract class PlayerMovement : CharacterMovement
{
    // hack !!!
    protected new Player Character => base.Character as Player;
}
