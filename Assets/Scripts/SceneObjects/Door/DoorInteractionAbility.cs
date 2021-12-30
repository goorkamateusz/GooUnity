using UnityEngine;

public class DoorInteractionAbility : KeyInputOrientedAbility
{
    [SerializeField] private CharacterColliderInteractions _interactions;

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
    }

    protected override void OnKeyUp()
    {
    }

    private void DoorTriggerEnter(Door obj)
    {
        _door = obj;
        _door.ColiderEnter();
    }

    private void DoorTriggerExit(Door obj)
    {
        _door.ColiderExit();
        _door = null;
    }
}