using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterMovement : CharacterComponent
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] private float _speedOriginal;

    private MovementTaskProvider _tasks = new MovementTaskProvider();
    private float _speedMultiplier = 1f;

    public bool IsEnabled { get; set; } = true;

    public NavMeshAgent Agent => _agent;
    public float Speed => _speedOriginal * _speedMultiplier;
    public float CurrentSpeed => _agent.velocity.magnitude;
    public float OriginalSpeed => _speedOriginal;

    public MovementTaskProvider Tasks => _tasks;

    public void SetMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
        UpdateAgent();
    }

    public void ResetMultiplier()
    {
        _speedMultiplier = 1f;
        UpdateAgent();
    }

    public void Stop()
    {
        _agent.ResetPath();
    }

    public virtual void Wrap(Transform target)
    {
        Wrap(target.position, target.rotation);
    }

    public void Wrap(Vector3 position, Quaternion rotation)
    {
        Agent.Warp(position);
        Character.transform.rotation = rotation;
    }

    protected abstract void HandleInput();

    protected virtual void Awake()
    {
        UpdateAgent();
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

    private void UpdateAgent()
    {
        _agent.speed = Speed;
    }
}
