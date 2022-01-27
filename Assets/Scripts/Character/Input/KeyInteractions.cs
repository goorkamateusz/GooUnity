using UnityEngine;

public class KeyInteractions : InteractionsProvider<InputKeyAction>
{
    public void CheckAll()
    {
        for (int i = _listener.Count - 1; i > -1; i--)
        {
            if (_listener[i].ProcessAction())
                _listener.RemoveAt(i);
        }
    }
}

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


public class SingleUseKeyHandler : PersistantKeyHandler
{
    protected override bool CancelAfterUp => false;
}
