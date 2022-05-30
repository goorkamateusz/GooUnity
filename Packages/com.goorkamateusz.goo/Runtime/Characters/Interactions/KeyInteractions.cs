namespace Goo.Characters.Interactions
{
    public class KeyInteractions : InteractionsProvider<InputKeyAction>
    {
        public void CheckAll()
        {
            for (int i = _listener.Count - 1; i > -1; i--)
            {
                if (_listener[i].ProcessAction())
                    _listener.RemoveAt(i);
            }
        }
    }
}