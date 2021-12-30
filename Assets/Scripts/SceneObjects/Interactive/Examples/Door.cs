using System;
using UnityEngine;
using UnityEngine.AI;

public class Door : SceneInteractiveElement
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

    [Header("Tips messages")]
    [SerializeField, TextArea] private string _tipOpenMessage;
    [SerializeField, TextArea] private string _tipCloseMessage;

    public bool IsOpen { get => _isOpen; private set => _isOpen = value; }
    public bool AutoOpen => _openAutomatically;
    public bool AutoClose => _openAutomatically;

    public override void ColiderEnter(IPlayerInteractiveComponent player)
    {
        if (AutoOpen)
            Open();
        else
            DisplayTip(player);
    }

    public override void ColiderExit(IPlayerInteractiveComponent player)
    {
        if (AutoClose)
            Close();
    }

    public override void OnKeyDown(IPlayerInteractiveComponent player)
    {
        Toggle();
        DisplayTip(player);
    }

    public override void OnKeyUp(IPlayerInteractiveComponent player)
    {
    }

    protected override void DisplayTip(IPlayerInteractiveComponent player)
    {
        DisplayTip(player, IsOpen ? _tipCloseMessage : _tipOpenMessage);
    }

    private void Close()
    {
        _door.transform.RotateAround(_hinge.position, _axis, -_angle);
        IsOpen = false;
        ToggleObstacle();
    }

    private void Open()
    {
        _door.transform.RotateAround(_hinge.position, _axis, _angle);
        IsOpen = true;
        ToggleObstacle();
    }

    private void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }

    private void ToggleObstacle()
    {
        if (_obstacle)
            _obstacle.enabled = !IsOpen;
    }

    protected void Start()
    {
        if (IsOpen)
            Open();
    }
}
