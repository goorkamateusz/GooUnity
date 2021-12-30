using System.Collections;
using UnityEngine;

public class ClassicDoor : DoorBase
{
    [Header("Animations")]
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _hinge;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _angle;

    protected override IEnumerator OpenAnimation()
    {
        return Rotate(-_angle);
    }

    protected override IEnumerator CloseAnimation()
    {
        return Rotate(_angle);
    }

    private IEnumerator Rotate(float angle)
    {
        int steps = 10;

        while (--steps > 0)
        {
            _door.transform.RotateAround(_hinge.position, _axis, angle / 10f);
            yield return new WaitForSeconds(0.1f);
        }

        _coroutine = null;
    }
}
