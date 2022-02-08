using UnityEngine;
using Goo.Tools;

public interface ICharacterInteractiveComponent
{
    public KeyCode Key { get; }
    public bool IsPlayer { get; }
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

    protected virtual void DisplayTip(ICharacterInteractiveComponent character, string msg)
    {
        if (character != null && character.IsPlayer && UiReferenceManager.Initialized)
            UiReferenceManager.Instance.KeyActionView.Null()?.DisplayTip(character.Key, msg);
    }

    protected virtual void HideTip(ICharacterInteractiveComponent character)
    {
        if (character != null && character.IsPlayer && UiReferenceManager.Initialized)
            UiReferenceManager.Instance.KeyActionView.Null()?.HideTip(character.Key);
    }
}
