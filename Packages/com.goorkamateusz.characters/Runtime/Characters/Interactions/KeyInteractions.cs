using UnityEngine;

namespace Goo.Characters.Interactions
{
    public class KeyInteractions : InteractionsProvider<InputKeyAction>
    {
        public void CheckAll()
        {
            for (int i = _listener.Count - 1; i > -1; i--)
            {
                if (ProcessAction(_listener[i]))
                    _listener.RemoveAt(i);
            }
        }

        private bool ProcessAction(InputKeyAction action)
        {
            if (Input.GetKeyDown(action.Key))
            {
                action.KeyDown();
                return action.CancelAfterDown;
            }

            if (Input.GetKey(action.Key))
            {
                action.KeyHold();
                return false;
            }

            if (Input.GetKeyUp(action.Key))
            {
                action.KeyUp();
                return action.CancelAfterUp;
            }

            return false;
        }
    }
}