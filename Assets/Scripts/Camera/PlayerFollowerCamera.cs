using System.Collections;
using UnityEngine;

public class PlayerFollowerCamera : SceneSingleton<PlayerFollowerCamera>
{
    [SerializeField] private float _smooth;
    [SerializeField] private Transform _camera;
    [SerializeField] private bool _followPlayerRotation;

    private Transform _target;

    public void SetStaticTransform(Vector3 offset, Quaternion rotation)
    {
        _camera.position = offset;
        _camera.rotation = rotation;
        _followPlayerRotation = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetDynamicTransform(Vector3 offset, Quaternion rotation)
    {
        // todo not working.
        _camera.position = offset;
        _camera.rotation = rotation;
        _followPlayerRotation = true;
    }

    private IEnumerator Start()
    {
        yield return PlayerManager.Wait();
        _target = PlayerManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        if (_target)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _smooth);

            if (_followPlayerRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _smooth);
            }
        }
    }
}
