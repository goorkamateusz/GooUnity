using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderInteractions : PlayerComponent
{
    private List<ColliderListener> _listener = new List<ColliderListener>();

    protected virtual void OnTriggerEnter(Collider other)
    {
        foreach (var listener in _listener)
            listener.CheckTriggerEnter(other);
    }

    public void AddListener(ColliderListener<InventoryItem> listener)
    {
        _listener.Add(listener);
    }
}

public abstract class ColliderListener
{
    public abstract void CheckTriggerEnter(Collider colider);
}

public class ColliderListener<T> : ColliderListener where T : UnityEngine.Object
{
    public event Action<T> OnTriggerEnter;
    public event Action<T> OnTriggerExit;

    public override void CheckTriggerEnter(Collider colider)
    {
        var item = colider.gameObject.GetComponent<T>();
        if (item)
            OnTriggerEnter?.Invoke(item);
    }
}
