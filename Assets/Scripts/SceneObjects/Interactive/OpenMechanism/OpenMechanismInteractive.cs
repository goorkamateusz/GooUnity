using UnityEngine;

public class OpenMechanismInteractive : SceneInteractiveElement
{
    [Header("Config")]
    [SerializeField] private bool _isOpen;
    [SerializeField] private bool _openAutomatically;
    [SerializeField] private bool _closeAutomatically;

    [Header("Tips messages")]
    [SerializeField, TextArea] protected string _tipOpenMessage = "Open door";
    [SerializeField, TextArea] protected string _tipCloseMessage = "Close door";

    [Header("Mechanism")]
    [SerializeField] private OpenMechanism[] _mechanism;

    public bool IsOpen => _isOpen;
    public bool AutoOpen => _openAutomatically;
    public bool AutoClose => _closeAutomatically;

    public override void ColiderEnter(ICharacterInteractiveComponent interaction)
    {
        if (AutoOpen) Open();
        DisplayTip(interaction);
    }

    public override void ColiderExit(ICharacterInteractiveComponent interaction)
    {
        if (AutoClose) Close();
        HideTip(interaction);
    }

    public override void OnKeyDown(ICharacterInteractiveComponent interaction)
    {
        Toggle();
        DisplayTip(interaction);
    }

    public override void OnKeyUp(ICharacterInteractiveComponent interaction)
    {
    }

    protected override void DisplayTip(ICharacterInteractiveComponent interaction)
    {
        DisplayTip(interaction, IsOpen ? _tipCloseMessage : _tipOpenMessage);
    }

    [ContextMenu("Close")]
    protected virtual void Close()
    {
        foreach (var mech in _mechanism)
            mech.Close();

        _isOpen = false;
        OnStateChange();
    }

    [ContextMenu("Open")]
    protected virtual void Open()
    {
        foreach (var mech in _mechanism)
            mech.Open();

        _isOpen = true;
        OnStateChange();
    }

    protected void Toggle()
    {
        if (IsOpen) Close();
        else Open();
    }

    protected virtual void OnStateChange()
    {
    }

    protected virtual void Start()
    {
        if (IsOpen)
            Open();
    }
}
