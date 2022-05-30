using System;
using UnityEngine;

namespace Goo.Characters.Interactions
{
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
}