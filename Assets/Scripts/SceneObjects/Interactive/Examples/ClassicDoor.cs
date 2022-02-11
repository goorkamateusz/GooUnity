using System.Collections;
using UnityEngine;

public class ClassicDoor : DoorBase
{
    [Header("Animations")]
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _hinge;
    [SerializeField] private Axis3.Enum _axis = Axis3.Enum.up;
    [SerializeField] private float _angle = 90f;
    [SerializeField] private float _angleSpeed = 90f;

    protected override IEnumerator OpenAnimation()
    {
        return Rotate(_angle);
    }

    protected override IEnumerator CloseAnimation()
    {
        return Rotate(0);
    }

    private IEnumerator Rotate(float targetAngle)
    {
        float change;

        Vector3 axis = Axis3.Translate(_axis);
        float currentAngle = Vector3.Dot(_door.localEulerAngles, axis);

        while (true)
        {
            change = _angleSpeed * Time.deltaTime;

            if (change > Mathf.Abs(targetAngle - currentAngle))
                break;

            if (currentAngle > targetAngle)
                change *= -1;

            currentAngle += change;

            if (_hinge == null)
                _door.localEulerAngles += axis * change;
            else
                _door.RotateAround(_hinge.position, axis, change);

            yield return null;
        }

        _door.localEulerAngles = axis * targetAngle;
        _coroutine = null;
    }
}
