using UnityEngine;

namespace Goo.Characters.Interactions
{
    public abstract class InputKeyAction
    {
        public KeyCode Key;

        internal virtual bool CancelAfterUp => false;
        internal virtual bool CancelAfterDown => false;

        internal abstract void KeyUp();
        internal abstract void KeyDown();

        internal virtual void KeyHold() { }
    }
}
