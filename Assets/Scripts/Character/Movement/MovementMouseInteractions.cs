using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementMouseInteractions
{
    private List<MovementMouseListener> _listener = new List<MovementMouseListener>();

    public void CheckAll(RaycastHit hit)
    {
        foreach (var listener in _listener)
        {
            listener.Check(hit);
        }
    }

    public void Add(MovementMouseListener action)
    {
        _listener.Add(action);
    }
}

public abstract class MovementMouseListener
{
    public abstract void Check(RaycastHit hit);
}

public class MovementMouseListener<T> : MovementMouseListener where T : UnityEngine.Object
{
    public event Action<T> Action;

    public override void Check(RaycastHit hit)
    {
        T target = hit.GetComponent<T>();
        if (target)
            Action?.Invoke(target);
    }
}
