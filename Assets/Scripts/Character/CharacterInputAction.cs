using UnityEngine;

public struct CharacterInputAction
{
    public delegate void OnKeyDelegate();
    public delegate void OnKeyUpDelegate();

    public KeyCode Key;
    public OnKeyDelegate OnKeyDown;
    public OnKeyUpDelegate OnKeyUp;

    public bool ProcessAction()
    {
        if (Input.GetKeyDown(Key))
        {
            if (OnKeyDown != null)
                OnKeyDown();
        }
        if (Input.GetKeyUp(Key))
        {
            if (OnKeyUp != null)
                OnKeyUp();
            return true;
        }
        return false;
    }
}
