using UnityEngine;

public abstract class InputKeyAction
{
    public KeyCode Key;

    protected virtual bool CancelAfterUp => false;
    protected virtual bool CancelAfterDown => false;

    public virtual bool ProcessAction()
    {
        if (Input.GetKeyDown(Key))
        {
            KeyDown();
            return CancelAfterDown;
        }

        if (Input.GetKey(Key))
        {
            KeyHold();
            return false;
        }

        if (Input.GetKeyUp(Key))
        {
            KeyUp();
            return CancelAfterUp;
        }

        return false;
    }

    protected abstract void KeyUp();
    protected abstract void KeyDown();

    protected virtual void KeyHold() { }
}
