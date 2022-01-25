using System;
using Goo.Tools;
using UnityEngine;

public class MouseInteractions : InteractionsProvider<MovementMouseListener>
{
    public void CheckAll(RaycastHit hit)
    {
        foreach (var listener in _listener)
        {
            listener.Check(hit);
        }
    }
}

public abstract class MovementMouseListener
{
    public abstract void Check(RaycastHit hit);
}

public class MovementMouseListener<T> : MovementMouseListener where T : UnityEngine.Object
{
    public event Action<T> Action;

    public MovementMouseListener() { }

    public MovementMouseListener(Action<T> initialAction)
    {
        Action += initialAction;
    }

    public override void Check(RaycastHit hit)
    {
        T target = hit.GetComponent<T>();
        if (target)
            Action?.Invoke(target);
    }
}
