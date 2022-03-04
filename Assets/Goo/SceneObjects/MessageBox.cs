using UnityEngine;

namespace Assets.Goo.SceneObjects
{
    public class MessageBox : SceneInteractiveElement
    {
        // IDEAS for messageBox's
        // [ ] Close buy UI button
        // [ ] Close buy key
        // [ ] Close on exit
        [SerializeField, TextArea] private string _message;

        public override void ColiderEnter(ICharacterInteraction character)
        {
            // todo desing UI on messages system
            // if (character.DisplayUI && UiReferenceManager.Initialized)
            //     UiReferenceManager.Instance.MessagePopup.Null()?.Open(_message, character);
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
}