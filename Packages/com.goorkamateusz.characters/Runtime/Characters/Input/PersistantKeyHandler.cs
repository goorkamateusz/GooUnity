public class PersistantKeyHandler : InputKeyAction
{
    public delegate void OnKeyDelegate();
    public delegate void OnKeyUpDelegate();

    public OnKeyDelegate OnKeyDown;
    public OnKeyUpDelegate OnKeyUp;

    protected override void KeyUp()
    {
        if (OnKeyUp != null)
            OnKeyUp();
    }

    protected override void KeyDown()
    {
        if (OnKeyDown != null)
            OnKeyDown();
    }
}
