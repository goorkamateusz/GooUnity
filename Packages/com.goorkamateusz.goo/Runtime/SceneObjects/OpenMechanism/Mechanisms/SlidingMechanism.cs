using System.Collections;
using UnityEngine;

namespace Goo.SceneObjects.OpenMechanism.Mechanisms
{
    public class SlidingMechanism : OpenMechanism
    {
        [SerializeField] protected Vector3 _move;
        [SerializeField] protected float _speed;

        protected virtual void Awake()
        {
            transform.localPosition = Vector3.zero;
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
            Vector3 direction = (targetPosition - transform.localPosition).normalized;

            while (true)
            {
                change = direction * Time.deltaTime * _speed;

                if (change.magnitude > (transform.localPosition - targetPosition).magnitude)
                    break;

                transform.localPosition += change;
                yield return null;
            }

            transform.localPosition = targetPosition;
            _coroutine = null;
        }
    }
}