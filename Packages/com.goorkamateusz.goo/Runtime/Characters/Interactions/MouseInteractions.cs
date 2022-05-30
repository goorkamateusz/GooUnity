using UnityEngine;

namespace Assets.Goo.Characters.Interactions
{
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
}