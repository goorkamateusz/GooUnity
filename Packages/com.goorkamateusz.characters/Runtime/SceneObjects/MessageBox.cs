using UnityEngine;
using Goo.Tools.EventSystem;

namespace Goo.SceneObjects
{
    public class MessageBox : SceneInteractiveElement
    {
        public struct EventMessageBox : IEvent<EventMessageBox>
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
            {
                var e = new EventMessageBox { Message = _message, Interaction = character };
                e.Send();
            }
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