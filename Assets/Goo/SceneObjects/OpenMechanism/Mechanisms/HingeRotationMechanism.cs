using System.Collections;
using UnityEngine;
using Assets.Goo.Tools.UnityHelpers;

namespace Assets.Goo.SceneObjects.OpenMechanism.Mechanisms
{
    public class HingeRotationMechanism : OpenMechanism
    {
        [SerializeField] protected Transform _hinge;
        [SerializeField] protected Axis3.Enum _axis = Axis3.Enum.up;
        [SerializeField, Range(-180, 180)] protected float _angle = 90f;
        [SerializeField] protected float _angleSpeed = 90f;

        protected virtual void Awake()
        {
            transform.localEulerAngles = Vector3.zero;
        }

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
            float currentAngle = Vector3.Dot(transform.localEulerAngles, axis);
            currentAngle = AngleHelper.From180To180(currentAngle);

            while (true)
            {
                change = _angleSpeed * Time.deltaTime;

                if (change > Mathf.Abs(targetAngle - currentAngle))
                    break;

                if (targetAngle - currentAngle < 0)
                    change *= -1;

                currentAngle += change;

                if (_hinge == null)
                    transform.localEulerAngles += axis * change;
                else
                    transform.RotateAround(_hinge.position, axis, change);

                yield return null;
            }

            transform.localEulerAngles = axis * targetAngle;
            _coroutine = null;
        }
    }
}