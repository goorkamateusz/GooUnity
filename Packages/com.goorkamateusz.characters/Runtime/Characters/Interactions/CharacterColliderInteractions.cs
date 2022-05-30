using System.Collections.Generic;
using UnityEngine;

namespace Goo.Characters.Interactions
{
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
}