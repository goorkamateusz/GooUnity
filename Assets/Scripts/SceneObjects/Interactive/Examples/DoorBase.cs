using UnityEngine;
using UnityEngine.AI;

public class DoorBase : SceneInteractiveElement
{
    [Header("Config")]
    [SerializeField] private bool _isOpen;
    [SerializeField] private bool _openAutomatically;
    [SerializeField] private bool _closeAutomatically;

    [Tooltip("If not null it disable obstacle for openned door")]
    [SerializeField] private NavMeshObstacle _obstacle;

    [Header("Tips messages")]
    [SerializeField, TextArea] private string _tipOpenMessage = "Open door";
    [SerializeField, TextArea] private string _tipCloseMessage = "Close door";

    [Header("Mechanism")]
    [SerializeField] private DoorMechanism[] _mechanism;

    public bool IsOpen { get => _isOpen; private set => _isOpen = value; }
    public bool AutoOpen => _openAutomatically;
    public bool AutoClose => _openAutomatically;

    public override void ColiderEnter(ICharacterInteractiveComponent character)
    {
        if (AutoOpen)
            Open();
        else
            DisplayTip(character);
    }

    public override void ColiderExit(ICharacterInteractiveComponent character)
    {
        if (AutoClose)
            Close();
        HideTip(character);
    }

    public override void OnKeyDown(ICharacterInteractiveComponent character)
    {
        Toggle();
        DisplayTip(character);
    }

    public override void OnKeyUp(ICharacterInteractiveComponent character)
    {
    }

    protected override void DisplayTip(ICharacterInteractiveComponent character)
    {
        DisplayTip(character, IsOpen ? _tipCloseMessage : _tipOpenMessage);
    }

    [ContextMenu("Close")]
    private void Close()
    {
        foreach (var mech in _mechanism)
            mech.Close();

        IsOpen = false;
        ToggleObstacle();
    }

    [ContextMenu("Open")]
    private void Open()
    {
        foreach (var mech in _mechanism)
            mech.Open();

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
