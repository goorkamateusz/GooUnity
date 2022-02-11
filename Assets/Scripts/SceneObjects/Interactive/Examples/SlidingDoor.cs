using System.Collections;
using UnityEngine;

// idea: two-side sliding doors
public class SlidingDoor : DoorBase
{
    [Header("Animations")]
    [SerializeField] private Transform _door;
    [SerializeField] private Vector3 _move;
    [SerializeField] private float _speed;

    protected virtual void Awake()
    {
        _door.position = Vector3.zero;
    }

    protected override IEnumerator OpenAnimation()
    {
        return Move(_move);
    }

    protected override IEnumerator CloseAnimation()
    {
        return Move(Vector3.zero);
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        Vector3 change;
        Vector3 direction = (targetPosition - _door.position).normalized;

        while (true)
        {
            change = direction * Time.deltaTime * _speed;

            if (change.magnitude > (_door.position - targetPosition).magnitude)
                break;

            _door.position += change;
            yield return null;
        }

        _door.position = targetPosition;
        _coroutine = null;
    }
}
