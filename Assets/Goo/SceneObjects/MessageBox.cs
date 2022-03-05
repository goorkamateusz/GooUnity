using Assets.Goo.Tools.EventSystem;
using UnityEngine;

namespace Assets.Goo.SceneObjects
{
    public class MessageBox : SceneInteractiveElement
    {
        public struct EventMessageBox
        {
            public string Message;
            public ICharacterInteraction Interaction;
        }

        // IDEAS for messageBox's
        // [ ] Close buy UI button
        // [ ] Close buy key
        // [ ] Close on exit
        [SerializeField, TextArea] private string _message;

        public override void ColiderEnter(ICharacterInteraction character)
        {
            if (character.DisplayUI)
                EventManager.Instance.Trigger(new EventMessageBox { Message = _message, Interaction = character });
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