using System.Collections.Generic;

public class KeyInteractions
{
    private List<CharacterInputAction> _actions = new List<CharacterInputAction>();

    public void Add(CharacterInputAction action)
    {
        _actions.Add(action);
    }

    public void Remove(CharacterInputAction action)
    {
        _actions.Remove(action);
    }

    public void CheckAll()
    {
        for (int i = _actions.Count - 1; i > -1; i--)
        {
            if (_actions[i].ProcessAction())
                _actions.RemoveAt(i);
        }
    }
}