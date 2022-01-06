using UnityEngine;

public class MessageBox : SceneInteractiveElement
{
    [SerializeField, TextArea] private string _message;

    public override void ColiderEnter(ICharacterInteractiveComponent character)
    {
        if (character.IsCharacter && UiReferenceManager.Initialized)
            UiReferenceManager.Instance.MessagePopup.Open(_message);
    }

    public override void ColiderExit(ICharacterInteractiveComponent character)
    {
    }

    public override void OnKeyDown(ICharacterInteractiveComponent character)
    {
    }

    public override void OnKeyUp(ICharacterInteractiveComponent character)
    {
    }
}
