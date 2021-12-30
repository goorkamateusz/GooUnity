using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _hinge;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _angle;

    [Header("Config")]
    [SerializeField] private bool _isOpen;
    [SerializeField] private bool _openAutomatically;
    [SerializeField] private bool _closeAutomatically;

    [Tooltip("If not null it disable obstacle for openned door")]
    [SerializeField] private NavMeshObstacle _obstacle;

    public bool IsOpen { get => _isOpen; private set => _isOpen = value; }
    public bool AutoOpen => _openAutomatically;
    public bool AutoClose => _openAutomatically;

    public void Close()
    {
        _door.transform.RotateAround(_hinge.position, _axis, -_angle);
        IsOpen = false;
        ToggleObstacle();
    }

    public void ColiderEnter()
    {
        if (AutoOpen)
            Open();
    }

    public void ColiderExit()
    {
        if (AutoClose)
            Close();
    }

    public void Open()
    {
        _door.transform.RotateAround(_hinge.position, _axis, _angle);
        IsOpen = true;
        ToggleObstacle();
    }

    public void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }

    protected void Start()
    {
        if (IsOpen)
            Open();
    }

    private void ToggleObstacle()
    {
        if (_obstacle)
            _obstacle.enabled = !IsOpen;
    }
}
