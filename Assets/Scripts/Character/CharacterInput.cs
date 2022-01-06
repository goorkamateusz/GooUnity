using System.Collections.Generic;
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

public class CharacterInput : MonoBehaviour
{
    private List<CharacterInputAction> _actions = new List<CharacterInputAction>();

    public void AddAction(CharacterInputAction action)
    {
        _actions.Add(action);
    }

    public void RemoveAction(CharacterInputAction action)
    {
        _actions.Remove(action);
    }

    protected void Update()
    {
        for (int i = _actions.Count - 1; i > -1; i--)
        {
            if (_actions[i].ProcessAction())
                _actions.RemoveAt(i);
        }
    }
}