using System.Collections;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private float _smooth;

    private Transform _target;
    private Vector3 _initialOffset;

    private IEnumerator Start()
    {
        yield return PlayerManager.Wait();
        _target = PlayerManager.Instance.Player.transform;
        _initialOffset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        if (_target)
        {
            Vector3 desiredPosition = _target.position + _initialOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _smooth);
        }
    }
}
