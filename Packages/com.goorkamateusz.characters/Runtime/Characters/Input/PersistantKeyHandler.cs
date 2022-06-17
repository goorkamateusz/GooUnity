namespace Goo.Characters.Interactions
{
    public class PersistantKeyHandler : InputKeyAction
    {
        public delegate void OnKeyDelegate();
        public delegate void OnKeyUpDelegate();

        public OnKeyDelegate OnKeyDown;
        public OnKeyUpDelegate OnKeyUp;

        internal override void KeyUp()
        {
            if (OnKeyUp != null)
                OnKeyUp();
        }

        internal override void KeyDown()
        {
            if (OnKeyDown != null)
                OnKeyDown();
        }
    }
}