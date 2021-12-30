using System;
public class MovementTask
{
    public Action Do;
    public Func<bool> Condition;

    public bool Check()
    {
        if (Condition.Invoke())
        {
            Do?.Invoke();
            return true;
        }
        return false;
    }
}