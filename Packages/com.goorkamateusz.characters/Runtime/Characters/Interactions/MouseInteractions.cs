using System;
using UnityEngine;

namespace Goo.Characters.Interactions
{
    public class MouseInteractions : InteractionsProvider<MovementMouseListener>
    {
        public event Action<RaycastHit> OnClick;

        public void CheckAll(RaycastHit hit)
        {
            OnClick?.Invoke(hit);
            foreach (var listener in _listener)
            {
                listener.Check(hit);
            }
        }
    }
}