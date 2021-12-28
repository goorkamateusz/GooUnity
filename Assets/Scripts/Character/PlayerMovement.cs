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
        _agent.SetDestination(_agent.transform.position);
    }

    protected abstract void HandleInput();

    protected virtual void Awake()
    {
        UpdateAgent();
    }

    protected void Update()
    {
        HandleInput();
    }

    private void UpdateAgent()
    {
        _agent.speed = Speed;
    }
}
