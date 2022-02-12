using Assets.Goo.SceneObjects;
using UnityEngine;

public class MessageBox : SceneInteractiveElement
{
    [SerializeField, TextArea] private string _message;

    public override void ColiderEnter(ICharacterInteraction character)
    {
        if (character.IsPlayer && UiReferenceManager.Initialized)
            UiReferenceManager.Instance.MessagePopup.Open(_message, character);
    }

    public override void ColiderExit(ICharacterInteraction character)
    {
    }

    public override void OnKeyDown(ICharacterInteraction character)
    {
    }

    public override void OnKeyUp(ICharacterInteraction character)
    {
    }
}
