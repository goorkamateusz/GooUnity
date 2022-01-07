using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterComponent : MonoBehaviour, ICharacterComponent
{
    protected Character Character { get; private set; }

    public void InjectCharacter(Character character)
    {
        Character = character;
    }
}

public abstract class CharacterMovement : CharacterComponent
{
    [SerializeField] protected NavMeshAgent _agent;

    [SerializeField] private float _speedOriginal;

    private float _speedMultiplier = 1f;

    public bool IsEnabled { get; set; } = true;

    public NavMeshAgent Agent => _agent;

    public float Speed => _speedOriginal * _speedMultiplier;

    public float CurrentSpeed => _agent.velocity.magnitude;
    public float OriginalSpeed => _speedOriginal;

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
        Agent.Warp(target.position);
        Character.transform.rotation = target.rotation;
    }

    public void Wrap(Vector3 position, Quaternion rotation)
    {
        Stop();
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
            HandleInput();
    }

    private void UpdateAgent()
    {
        _agent.speed = Speed;
    }
}
