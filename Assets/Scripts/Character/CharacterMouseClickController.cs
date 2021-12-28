using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMouseClickController : Ability
{
    [Header("Mouse")]
    [SerializeField] private int _mouseButtonNumber = 0;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Animations")]
    [SerializeField] private double _minimalVelocity = 0.6;

    private Camera _main;

    private void Awake()
    {
        _main = Camera.main;
    }

    private void Update()
    {
        if (_agent.velocity.magnitude < _minimalVelocity)
            Player.AnimatorHandler.SetMoveSpeed(0f);

        if (Input.GetMouseButtonDown(_mouseButtonNumber))
        {
            var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
            {
                _agent.SetDestination(hit.point);
                Player.AnimatorHandler.SetMoveSpeed(0.5f);
            }
        }
    }
}
