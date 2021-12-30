using System;
using UnityEngine;

public class CameraChangeAbility : KeyInputOrientedAbility
{
    [Serializable]
    public class CameraSettings
    {
        public Vector3 Offset;
        public Vector3 Orientation;
        public bool FollowPlayerRotation;

        public Quaternion Rotation => Quaternion.Euler(Orientation);
        // public Quaternion Rotation => Quaternion.LookRotation(Orientation);
    }

    [SerializeField] private CameraSettings[] _settings;
    [SerializeField] private int _initialState;

    protected override void OnKeyDown()
    {
        _initialState = (_initialState + 1) % _settings.Length;
        var setting = _settings[_initialState];

        if (setting.FollowPlayerRotation)
        {
            // todo not working
            PlayerFollowerCamera.Instance.SetDynamicTransform(setting.Offset, setting.Rotation);
        }
        else
        {
            PlayerFollowerCamera.Instance.SetStaticTransform(setting.Offset, setting.Rotation);
        }
    }

    protected override void OnKeyUp()
    {
    }
}
