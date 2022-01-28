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
        Vector2 difference = new Vector2(1 - 2 * Input.mousePosition.x / Screen.width,
                                         1 - 2 * Input.mousePosition.y / Screen.height);

        float angle = difference.x * difference.x * difference.x * Time.deltaTime;

        if (_lock)
        {
            _cameraParent.localRotation = Quaternion.Euler(difference.y * _cameraTopDownMaximumAmplitude, difference.x * 180, 0);
        }
        else
        {
            _cameraParent.localRotation = Quaternion.Euler(difference.y * _cameraTopDownMaximumAmplitude, 0, 0);
            transform.Rotate(Vector3.up, -angle * _characterAngelVelocity);
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
