using UnityEngine;

public class SingleSceneInteractionAbility : KeyInputOrientedAbility, ICharacterInteractiveComponent
{
    [SerializeField] private CharacterColliderInteractions _interactions;

    private SceneInteractiveElement _object;

    public virtual bool IsCharacter => true;
    public KeyCode Key => _key;
    public new Character Character => base.Character;

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
