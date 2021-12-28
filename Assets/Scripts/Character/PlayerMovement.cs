using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent : MonoBehaviour, IPlayerComponent
{
    protected Player Player { get; private set; }

    public void InjectPlayer(Player player)
    {
        Player = player;
    }
}

public abstract class PlayerMovement : PlayerComponent
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

    protected abstract void HandleInput();

    protected virtual void Awake()
    {
        UpdateAgent();
    }

    protected void Update()
    {
        if (IsEnabled)
            HandleInput();
    }

    private void UpdateAgent()
    {
        _agent.speed = Speed;
    }
}
