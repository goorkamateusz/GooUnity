using UnityEngine;
using Goo.Characters;
using Goo.Tools.EventSystem;

namespace Goo.SceneObjects
{
    public interface ICharacterInteraction
    {
        public bool DisplayUI { get; }
        public KeyCode Key { get; }
        public Character Character { get; }
    }

    // todo separate colider entry
    // todo colider mouse click
    [RequireComponent(typeof(Collider))]
    public abstract class SceneInteractiveElement : MonoBehaviour
    {
        public struct Event : IEvent<Event>
        {
            public bool Hide;
            public KeyCode Key;
            public string Messsage;
        }

        public abstract void ColiderEnter(ICharacterInteraction character);
        public abstract void ColiderExit(ICharacterInteraction character);

        public abstract void OnKeyDown(ICharacterInteraction character);
        public abstract void OnKeyUp(ICharacterInteraction character);

        protected virtual void DisplayTip(ICharacterInteraction character)
        {
            DisplayTip(character, string.Empty);
        }

        protected virtual void DisplayTip(ICharacterInteraction character, string msg)
        {
            if (character.DisplayUI)
            {
                var e = new Event { Key = character.Key, Messsage = msg };
                e.Send();
            }
        }

        protected virtual void HideTip(ICharacterInteraction character)
        {
            if (character.DisplayUI)
            {
                var e = new Event { Key = character.Key, Hide = true };
                e.Send();
            }
        }
    }
}