using UnityEngine;

public class MessageBox : SceneInteractiveElement
{
    [SerializeField, TextArea] private string _message;

    public override void ColiderEnter(ICharacterInteractiveComponent character)
    {
        Debug.Log("MessageBox");
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
