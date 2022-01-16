using System.Collections.Generic;

public class InteractionsProvider<T>
{
    protected List<T> _listener = new List<T>();

    public void Add(T action) => _listener.Add(action);
    public void Remove(T action) => _listener.Remove(action);
}
