using UnityEngine;

public interface ICharacterInteractiveComponent
{
    public KeyCode Key { get; }
    public bool IsCharacter { get; }
    public Character Character { get; }
}

[RequireComponent(typeof(Collider))]
public abstract class SceneInteractiveElement : MonoBehaviour
{
    public abstract void ColiderEnter(ICharacterInteractiveComponent character);
    public abstract void ColiderExit(ICharacterInteractiveComponent character);

    public abstract void OnKeyDown(ICharacterInteractiveComponent character);
    public abstract void OnKeyUp(ICharacterInteractiveComponent character);

    protected virtual void DisplayTip(ICharacterInteractiveComponent character)
    {
        DisplayTip(character, string.Empty);
    }

    protected void DisplayTip(ICharacterInteractiveComponent character, string msg)
    {
        if (character?.IsCharacter ?? false && UiReferenceManager.Initialized)
            UiReferenceManager.Instance?.KeyActionView.DisplayTip(character.Key, msg);
    }

    protected void HideTip(ICharacterInteractiveComponent character)
    {
        if (character?.IsCharacter ?? false && UiReferenceManager.Initialized)
            UiReferenceManager.Instance.KeyActionView.HideTip(character.Key);
    }
}
