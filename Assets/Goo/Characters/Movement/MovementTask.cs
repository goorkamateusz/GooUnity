using System;

public class MovementTask
{
    // todo separate more efficient abstract
    public Action Do;
    public Action Otherwise;
    public Func<bool> Condition;
    public bool DisableAutoDelete;

    public bool Check()
    {
        if (Condition.Invoke())
        {
            Do?.Invoke();
            return true;
        }
        Otherwise?.Invoke();
        return false;
    }
}