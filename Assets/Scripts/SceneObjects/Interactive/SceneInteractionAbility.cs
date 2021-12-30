using UnityEngine;

public class SceneInteractionAbility : KeyInputOrientedAbility, IPlayerInteractiveComponent
{
    [SerializeField] private CharacterColliderInteractions _interactions;

    private SceneInteractiveElement _object;

    public KeyCode Key => _key;

    protected override void Start()
    {
        base.Start();
        _interactions.AddListener(new ColliderListener<SceneInteractiveElement>(DoorTriggerEnter, DoorTriggerExit));
    }

    protected override void OnKeyDown()
    {
        _object?.OnKeyDown(this);
    }

    protected override void OnKeyUp()
    {
        _object?.OnKeyUp(this);
    }

    protected virtual void DoorTriggerEnter(SceneInteractiveElement obj)
    {
        _object = obj;
        _object.ColiderEnter(this);
    }

    protected virtual void DoorTriggerExit(SceneInteractiveElement obj)
    {
        _object.ColiderExit(this);
        _object = null;
    }
}
