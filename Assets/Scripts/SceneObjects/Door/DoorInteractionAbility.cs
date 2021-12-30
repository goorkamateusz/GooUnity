using System;
using UnityEngine;

public class DoorInteractionAbility : KeyInputOrientedAbility
{
    [SerializeField] private CharacterColliderInteractions _interactions;
    [SerializeField, TextArea] private string _tipOpenMessage;
    [SerializeField, TextArea] private string _tipCloseMessage;

    private ColliderListener<Door> _doorListener = new ColliderListener<Door>();
    private Door _door;

    protected override void Start()
    {
        base.Start();
        _doorListener.OnTriggerEnter += DoorTriggerEnter;
        _doorListener.OnTriggerExit += DoorTriggerExit;
        _interactions.AddListener(_doorListener);
    }

    protected override void OnKeyDown()
    {
        _door?.Toggle();
        DisplayTip();
    }

    protected override void OnKeyUp()
    {
    }

    private void DoorTriggerEnter(Door obj)
    {
        _door = obj;
        _door.ColiderEnter();
        DisplayTip();
    }

    private void DoorTriggerExit(Door obj)
    {
        _door.ColiderExit();
        _door = null;
        HideTip();
    }

    private void DisplayTip()
    {
        if (UiReferenceManager.Initialized && _door)
            UiReferenceManager.Instance?.KeyActionView.DisplayTip(_key, _door.IsOpen ? _tipCloseMessage : _tipOpenMessage);
    }

    private void HideTip()
    {
        if (UiReferenceManager.Initialized)
            UiReferenceManager.Instance.KeyActionView.HideTip(_key);
    }
}