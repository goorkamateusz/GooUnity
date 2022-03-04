using Assets.Goo.Characters;
using UnityEngine;

namespace Assets.Goo.SceneObjects.Characters
{
    public class SceneInteractionAbility : KeyInputOrientedAbility, ICharacterInteraction
    {
        // bug when colliders overlaps
        private SceneInteractiveElement _object;

        public KeyCode Key => _key;
        public bool DisplayUI => true;
        Character ICharacterInteraction.Character => Character;

        protected override void OnStart()
        {
            base.OnStart();
            Character.ColliderInteractions.Add(new ColliderListener<SceneInteractiveElement>(TriggerEnter, TriggerExit));
        }

        protected override void OnKeyDown()
        {
            _object?.OnKeyDown(this);
        }

        protected override void OnKeyUp()
        {
            _object?.OnKeyUp(this);
        }

        protected virtual void TriggerEnter(SceneInteractiveElement obj)
        {
            _object = obj;
            _object.ColiderEnter(this);
        }

        protected virtual void TriggerExit(SceneInteractiveElement obj)
        {
            _object.ColiderExit(this);
            _object = null;
        }
    }
}