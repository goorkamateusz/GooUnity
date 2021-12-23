using UnityEngine;
using UnityEngine.AI;

public class RunAbility : KeyInputOrientedAbility
{
    private const string Name = "Run";

    [Header("Run Ability")]
    [SerializeField] private float _speedMultiplier;

    private NavMeshAgent _agent;
    private float _originalSpeed;

    protected override void Start()
    {
        base.Start();
        _agent = Player.gameObject.GetComponent<NavMeshAgent>();
    }

    protected override void OnKeyDown()
    {
        _originalSpeed = _agent.speed;
        _agent.speed *= _speedMultiplier;
        Player.Animator.SetBool(Name, true);
    }

    protected override void OnKeyUp()
    {
        _agent.speed = _originalSpeed;
        Player.Animator.SetBool(Name, false);
    }
}
