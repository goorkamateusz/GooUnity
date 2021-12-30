using System.Collections;
using UnityEngine;

public class SlidingDoor : DoorBase
{
    [Header("Animations")]
    [SerializeField] private Transform _door;
    [SerializeField] private Vector3 _move;

    protected override IEnumerator OpenAnimation()
    {
        return Move(_move);
    }

    protected override IEnumerator CloseAnimation()
    {
        return Move(-_move);
    }

    private IEnumerator Move(Vector3 move)
    {
        int steps = 10;
        Vector3 startPosition = _door.position;
        Vector3 endPosition = startPosition + move;

        while (--steps > 0)
        {
            _door.position = Vector3.Lerp(startPosition, endPosition, (10 - steps) / 10f);
            yield return new WaitForSeconds(0.1f);
        }

        _coroutine = null;
    }
}
