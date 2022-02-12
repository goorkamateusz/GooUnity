using System.Collections;
using Assets.Goo.Tools.Patterns;
using UnityEngine;

public class PlayerFollowerCamera : SceneSingleton<PlayerFollowerCamera>
{
    [SerializeField] private Camera _camera;

    private Transform _target;
    private float _smooth;
    private bool _followPlayerRotation;

    public void SetConfig(ICameraSettings settings)
    {
        settings.ConfigCamera(_camera);
        _smooth = settings.Smooth;
        _followPlayerRotation = settings.FollowPlayerRotation;

        if (!_followPlayerRotation)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
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
                transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _smooth);
        }
    }
}
