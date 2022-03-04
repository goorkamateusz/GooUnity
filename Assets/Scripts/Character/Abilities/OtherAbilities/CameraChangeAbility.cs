using Assets.Goo.Characters.Ability;
using UnityEngine;

public class CameraChangeAbility : KeyInputOrientedAbility
{
    [SerializeField] private CameraReferencedSettings[] _settings;
    [SerializeField] private int _initialState;

    protected override void OnStart()
    {
        base.OnStart();
        SetCameraSettings();
    }

    protected override void OnKeyDown()
    {
        _initialState = (_initialState + 1) % _settings.Length;
        SetCameraSettings();
    }

    protected override void OnKeyUp()
    {
    }

    private void SetCameraSettings()
    {
        var setting = _settings[_initialState];
        PlayerFollowerCamera.Instance.SetConfig(setting);
    }
}
