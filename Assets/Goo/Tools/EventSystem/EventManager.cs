using System;
using System.Collections.Generic;
using Assets.Goo.Tools.Patterns;

namespace Assets.Goo.Tools.EventSystem
{
    internal class EventManager : Singleton<EventManager>
    {
        private Dictionary<Type, List<IEventListener>> subscribers = new Dictionary<Type, List<IEventListener>>();

        internal void Subscribe<T>(IEventListener<T> listener)
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

        internal void Unsubscribe<T>(IEventListener<T> listener)
        {
            if (subscribers.TryGetValue(typeof(T), out var list))
                list?.Remove(listener);
        }

        internal bool IsSubscriber<T>(IEventListener<T> listener)
        {
            if (subscribers.TryGetValue(typeof(T), out var list))
                return list.Contains(listener);
            return false;
        }

        internal void Trigger<T>(T e)
        {
            if (subscribers.TryGetValue(typeof(T), out var list))
            {
                foreach (var listener in list)
                {
                    (listener as IEventListener<T>).OnTrigger(e);
                }
            }
        }
    }
}