using UnityEngine;

public class SceneInteractionAbility : KeyInputOrientedAbility, IPlayerInteractiveComponent
{
    [SerializeField] private CharacterColliderInteractions _interactions;

    private SceneInteractiveElement _object;

    public virtual bool IsPlayer => true;
    public KeyCode Key => _key;

    protected override void Start()
    {
        base.Start();
        _interactions.AddListener(new ColliderListener<SceneInteractiveElement>(TriggerEnter, TriggerExit));
    }

    protected override void OnKeyDown()
    {
        _object?.OnKeyDown(this);
    }

    protected override void OnKeyUp()
    {
        _object?.OnKeyUp(this);
    }

    protected virtual void TriggerEnter(SceneInteractiveElement obj)
    {
        _object = obj;
        _object.ColiderEnter(this);
    }

    protected virtual void TriggerExit(SceneInteractiveElement obj)
    {
        _object.ColiderExit(this);
        _object = null;
    }
}
