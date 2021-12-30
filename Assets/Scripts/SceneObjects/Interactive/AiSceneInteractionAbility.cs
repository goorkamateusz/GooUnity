using UnityEngine;

public class AiSceneInteractionAbility : SceneInteractionAbility
{
    [SerializeField] private bool _autoOpenDoors;

    protected override void TriggerEnter(SceneInteractiveElement obj)
    {
        base.TriggerEnter(obj);
        ToggleActions(obj);
    }

    protected override void TriggerExit(SceneInteractiveElement obj)
    {
        base.TriggerExit(obj);
        ToggleActions(obj);
    }

    private void ToggleActions(SceneInteractiveElement obj)
    {
        if (_autoOpenDoors && obj is DoorBase)
            obj?.OnKeyDown(null);
    }
}
