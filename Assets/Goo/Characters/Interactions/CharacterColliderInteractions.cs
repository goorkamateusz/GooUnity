using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderInteractions : MonoBehaviour, IInteractionsProvider<ColliderListener>
{
    protected List<ColliderListener> _listener = new List<ColliderListener>();

    protected virtual void OnTriggerEnter(Collider colider)
    {
        foreach (var listener in _listener)
            listener.CheckTriggerEnter(colider);
    }

    protected virtual void OnTriggerExit(Collider colider)
    {
        foreach (var listener in _listener)
            listener.CheckTriggerExit(colider);
    }

    public void Add(ColliderListener listener) => _listener.Add(listener);
    public void Remove(ColliderListener action) => _listener.Remove(action);
}

public abstract class ColliderListener
{
    public abstract void CheckTriggerEnter(Collider colider);
    public abstract void CheckTriggerExit(Collider colider);
}

public class ColliderListener<T> : ColliderListener where T : UnityEngine.Object
{
    public event Action<T> OnTriggerEnter;
    public event Action<T> OnTriggerExit;

    public ColliderListener() { }

    public ColliderListener(Action<T> enter, Action<T> exit)
    {
        if (enter != null)
            OnTriggerEnter += enter;
        if (exit != null)
            OnTriggerExit += exit;
    }

    public override void CheckTriggerEnter(Collider colider)
    {
        var item = colider.gameObject.GetComponent<T>();
        if (item)
            OnTriggerEnter?.Invoke(item);
    }

    public override void CheckTriggerExit(Collider colider)
    {
        var item = colider.gameObject.GetComponent<T>();
        if (item)
            OnTriggerExit?.Invoke(item);
    }
}
