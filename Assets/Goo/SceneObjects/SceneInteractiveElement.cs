using UnityEngine;
using Assets.Goo.Tools.UnityHelpers;

namespace Assets.Goo.SceneObjects
{
    public interface ICharacterInteraction
    {
        public bool DisplayUI { get; }
        public KeyCode Key { get; }
        public Character Character { get; }
    }

    [RequireComponent(typeof(Collider))]
    public abstract class SceneInteractiveElement : MonoBehaviour
    {
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
            if (character.DisplayUI && UiReferenceManager.Initialized)
                UiReferenceManager.Instance.KeyActionView.Null()?.DisplayTip(character.Key, msg);
        }

        protected virtual void HideTip(ICharacterInteraction character)
        {
            if (character.DisplayUI && UiReferenceManager.Initialized)
                UiReferenceManager.Instance.KeyActionView.Null()?.HideTip(character.Key);
        }
    }
}