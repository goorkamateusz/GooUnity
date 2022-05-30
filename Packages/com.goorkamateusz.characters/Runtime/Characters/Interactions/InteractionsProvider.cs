using System.Collections.Generic;

namespace Goo.Characters.Interactions
{
    public interface IInteractionsProvider<T>
    {
        void Add(T action);
        void Remove(T action);
    }
    
    public class InteractionsProvider<T> : IInteractionsProvider<T>
    {
        protected List<T> _listener = new List<T>();

        public void Add(T action) => _listener.Add(action);
        public void Remove(T action) => _listener.Remove(action);
    }
}