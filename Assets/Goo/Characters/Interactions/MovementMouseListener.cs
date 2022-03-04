using System;
using UnityEngine;
using Assets.Goo.Tools.UnityHelpers;

namespace Assets.Goo.Characters.Interactions
{
    public abstract class MovementMouseListener
    {
        public abstract void Check(RaycastHit hit);
    }

    public class MovementMouseListener<T> : MovementMouseListener where T : UnityEngine.Object
    {
        public event Action<T> Action;

        public MovementMouseListener() { }

        public MovementMouseListener(Action<T> initialAction)
        {
            Action += initialAction;
        }

        public override void Check(RaycastHit hit)
        {
            T target = hit.GetComponent<T>();
            if (target)
                Action?.Invoke(target);
        }
    }
}