using UnityEngine;

public class Ability : CharacterComponent
{
}

public abstract class KeyInputOrientedAbility : Ability
{
    [SerializeField] protected KeyCode _key;

    private InputKeyAction _action;

    protected override void OnStart()
    {
        _action = new PersistantKeyHandler
        {
            Key = _key,
            OnKeyDown = OnKeyDown,
            OnKeyUp = OnKeyUp
        };
        Character.Input.KeyInteractions.Add(_action);
    }

    protected abstract void OnKeyDown();
    protected abstract void OnKeyUp();
}
