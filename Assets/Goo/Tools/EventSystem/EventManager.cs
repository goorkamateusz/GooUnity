using System;
using System.Collections.Generic;
using Assets.Goo.Tools.Patterns;

namespace Assets.Goo.Tools.EventSystem
{
    public interface IEventListener
    {
    }

    public interface IEventListener<T> : IEventListener
    {
        void OnTrigger(T e);
    }

    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<Type, List<IEventListener>> subscribers = new Dictionary<Type, List<IEventListener>>();

        public void Subscribe<T>(IEventListener<T> listener)
        {
            Type eventType = typeof(T);
            List<IEventListener> list;

            if (!subscribers.TryGetValue(eventType, out list))
            {
                list = new List<IEventListener>();
                subscribers[eventType] = list;
            }

            /// idae validation of duplications
            list.Add(listener);
        }

        public void Unsubscribe<T>(IEventListener<T> listener)
        {
            Type eventType = typeof(T);

            if (subscribers.TryGetValue(eventType, out var list))
            {
                list?.Remove(listener);
            }
        }

        public void Trigger<T>(T e)
        {
            Type eventType = typeof(T);

            if (subscribers.TryGetValue(eventType, out var list))
            {
                foreach (var listener in list)
                {
                    (listener as IEventListener<T>).OnTrigger(e);
                }
            }
        }
    }
}