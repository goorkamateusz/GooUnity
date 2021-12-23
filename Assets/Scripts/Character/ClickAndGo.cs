using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickAndGo : MonoBehaviour
{
    private const string Name = "Walk";
    private const double MinimalVelocity = 0.05;

    [SerializeField] private Animator _animator;

    private NavMeshAgent _agent;
    private Camera _main;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _main = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
            {
                _agent.SetDestination(hit.point);
            }
        }

        _animator.SetBool(Name, _agent.velocity.magnitude > MinimalVelocity);
    }
}
