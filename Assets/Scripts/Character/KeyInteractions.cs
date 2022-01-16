using UnityEngine;

public class KeyInteractions : InteractionsProvider<CharacterInputAction>
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
