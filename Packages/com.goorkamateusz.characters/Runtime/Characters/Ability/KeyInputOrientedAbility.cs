using Goo.Characters.Interactions;
using UnityEngine;

namespace Goo.Characters.Ability
{
    public abstract class KeyInputOrientedAbility : Ability
    {
        [SerializeField] protected KeyCode _key;

        private InputKeyAction _action;

        protected override void OnStart()
        {
            _action = new PersistantKeyHandler
            {
                Key = _key,
                OnKeyDown = OnKeyDown,
                OnKeyUp = OnKeyUp
            };
            Character.Input.KeyInteractions.Add(_action);
        }

        protected abstract void OnKeyDown();
        protected abstract void OnKeyUp();
    }
}