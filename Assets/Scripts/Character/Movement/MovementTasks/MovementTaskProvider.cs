using System.Collections.Generic;

public class MovementTaskProvider
{
    private List<MovementTask> _tasks = new List<MovementTask>();
    private List<int> _done = new List<int>();

    public void Add(MovementTask task)
    {
        _tasks.Add(task);
    }

    public bool CheckAll()
    {
        _done.Clear();

        // todo deleting form list
        for (int i = 0; i < _tasks.Count; i++)
        {
            if (_tasks[i].Check() && !_tasks[i].DisableAutoDelete)
                _done.Add(i);
        }

        foreach (var doneId in _done)
            _tasks.RemoveAt(doneId);

        return _done.Count > 0;
    }

    public void Clear()
    {
        _tasks.Clear();
    }
}