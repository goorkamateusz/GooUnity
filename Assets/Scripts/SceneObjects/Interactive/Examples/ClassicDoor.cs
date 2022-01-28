using System.Collections;
using UnityEngine;

public class ClassicDoor : DoorBase
{
    [Header("Animations")]
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _hinge;
    [SerializeField] private Vector3 _axis = Vector3.up;
    [SerializeField] private float _angle = 90f;

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
            if (_hinge != null)
            {
                _door.transform.RotateAround(_hinge.position, _axis, angle / 10f);
            }
            else
            {
                _door.transform.Rotate(_axis, angle / 10f);
                // bug that case not working correct
            }
            yield return new WaitForSeconds(0.1f);
        }

        _coroutine = null;
    }
}
