using System.Threading.Tasks;
using UnityEngine;

public class Character3rdPersonRotate : CharacterComponent
{
    [SerializeField] private Transform _cameraParent;
    [SerializeField] private Character3rdPersonMovement _movement;
    [SerializeField] private float _characterAngelVelocity;
    [SerializeField] private float _cameraTopDownMaximumAmplitude = 30f;
    [SerializeField] private KeyCode _cameraModeKey = KeyCode.LeftShift;
    [SerializeField] private Vector3 _cameraPositionScale = new Vector3(1, 1.5f, 3);
    [SerializeField] private float _modeChangeDuration = 1.5f;

    private PersistantKeyHandler _rotateToggle;
    private bool _lock = false;
    private Vector2 _referencePoint;
    private Vector2 _centerDistance;

    protected override void OnStart()
    {
        _rotateToggle = new PersistantKeyHandler
        {
            Key = _cameraModeKey,
            OnKeyDown = TurnOnCameraToggle,
            OnKeyUp = TurnOffCameraToggle
        };
        Character.Input.KeyInteractions.Add(_rotateToggle);
    }

    protected virtual void Update()
    {
        _centerDistance = new Vector2(1 - 2 * Input.mousePosition.x / Screen.width,
                                      1 - 2 * Input.mousePosition.y / Screen.height);

        if (_lock)
        {
            Vector2 deflection = _centerDistance - _referencePoint;
            _cameraParent.localRotation = Quaternion.Euler(deflection.y * _cameraTopDownMaximumAmplitude, deflection.x * 180, 0);
        }
        else
        {
            float angle = _centerDistance.x * _centerDistance.x * _centerDistance.x * Time.deltaTime;
            _cameraParent.localRotation = Quaternion.Euler(_centerDistance.y * _cameraTopDownMaximumAmplitude, 0, 0);
            Character.transform.Rotate(Vector3.up, -angle * _characterAngelVelocity);
        }
    }

    private async void TurnOffCameraToggle()
    {
        _lock = false;
        await SmoothChange(Vector3.one);
    }

    private async void TurnOnCameraToggle()
    {
        _lock = true;
        _referencePoint = _centerDistance;
        await SmoothChange(_cameraPositionScale);
    }

    private async Task SmoothChange(Vector3 _cameraPositionScale)
    {
        float timer = 0f;
        while (timer < _modeChangeDuration)
        {
            timer += Time.deltaTime;
            _cameraParent.localScale = Vector3.Lerp(_cameraParent.localScale, _cameraPositionScale, timer / _modeChangeDuration);
            await Task.Yield();
        }
        _cameraParent.localScale = _cameraPositionScale;
    }
}
