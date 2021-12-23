using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smooth;

    private Vector3 _initialOffset;

    private void Start()
    {
        _initialOffset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + _initialOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smooth);
    }
}
