using System.Collections.Generic;

public class MovementTaskProvider
{
    private List<MovementTask> _tasks = new List<MovementTask>();

    public void Add(MovementTask task)
    {
        _tasks.Add(task);
    }

    public bool CheckAll()
    {
        bool anyDone = false;
        
        for (int i = _tasks.Count - 1; i > -1; --i)
        {
            if (_tasks[i].Check() && !_tasks[i].DisableAutoDelete)
            {
                _tasks.RemoveAt(i);
                anyDone = true;
            }
        }

        return anyDone;
    }

    public void Clear()
    {
        _tasks.Clear();
    }
}