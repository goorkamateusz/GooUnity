using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMouseClickController : MonoBehaviour
{
    [Header("Mouse")]
    [SerializeField] private int _mouseButtonNumber = 0;

    [Header("Animations")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolParameterName = "Walk";
    [SerializeField] private double _minimalVelocity = 0.6;

    private NavMeshAgent _agent;
    private Camera _main;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _main = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_mouseButtonNumber))
        {
            var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
            {
                _agent.SetDestination(hit.point);
            }
        }

        _animator.SetBool(_boolParameterName, _agent.velocity.magnitude > _minimalVelocity);
    }
}
