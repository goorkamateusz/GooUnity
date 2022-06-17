using System;
using UnityEngine;

namespace Goo.Characters.Interactions
{
    public class MouseInteractions : InteractionsProvider<MovementMouseListener>
    {
        private const int Button = 0; // todo temp

        public event Action<RaycastHit> OnClick;

        private Camera _main;
        private RaycastHit _hit;

        public RaycastHit Hit => _hit;
        public bool Clicked { get; private set; }

        internal void SetCamera(Camera camera)
        {
            _main = camera;
        }

        internal virtual void HandleInput()
        {
            Clicked = false;

            if (Input.GetMouseButtonDown(Button))
            {
                var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
                {
                    _hit = hit;
                    Clicked = true;
                }
            }
        }

        internal virtual void HandleLateUpdate()
        {
            if (Clicked)
            {
                CheckAll(_hit);
            }
        }

        private void CheckAll(RaycastHit hit)
        {
            OnClick?.Invoke(hit);
            foreach (var listener in _listener)
            {
                listener.Check(hit);
            }
        }
    }
}