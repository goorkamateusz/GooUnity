using UnityEngine;

public class AiSceneInteractionAbility : SingleSceneInteractionAbility
{
    [SerializeField] private bool _autoOpenDoors;

    public override bool IsCharacter => false;

    protected override void TriggerEnter(SceneInteractiveElement obj)
    {
        base.TriggerEnter(obj);
        Enter(obj);
    }

    protected override void TriggerExit(SceneInteractiveElement obj)
    {
        base.TriggerExit(obj);
        Exit(obj);
    }

    private void Enter(SceneInteractiveElement obj)
    {
        if (_autoOpenDoors)
            OpenDoor(obj as DoorBase);
    }

    private void Exit(SceneInteractiveElement obj)
    {
        if (_autoOpenDoors)
            CloseDoor(obj as DoorBase);
    }

    private void CloseDoor(DoorBase door)
    {
        if (door is null)
            return;

        if (door.IsOpen)
            door.OnKeyDown(this);
    }

    private void OpenDoor(DoorBase door)
    {
        if (door is null)
            return;

        if (!door.IsOpen)
            door.OnKeyDown(this);
    }
}
