using UnityEngine;

public class AiSceneInteractionAbility : SceneInteractionAbility
{
    [SerializeField] private bool _autoOpenDoors;

    protected override void DoorTriggerEnter(SceneInteractiveElement obj)
    {
        base.DoorTriggerEnter(obj);
        ToggleActions(obj);
    }

    protected override void DoorTriggerExit(SceneInteractiveElement obj)
    {
        base.DoorTriggerExit(obj);
        ToggleActions(obj);
    }

    private void ToggleActions(SceneInteractiveElement obj)
    {
        if (_autoOpenDoors && obj is DoorBase)
            obj?.OnKeyDown(null);
    }
}
