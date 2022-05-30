using Assets.Goo.Characters;
using Assets.Goo.Characters.Interactions;
using UnityEngine;

namespace Assets.Goo.SceneObjects.Characters
{
    public class AutoSceneInteractionAbility : CharacterComponent, ICharacterInteraction
    {
        [SerializeField] private AutoSceneInteractionDefinitions[] _definitions;

        public KeyCode Key => KeyCode.None;
        public bool DisplayUI => false;
        Character ICharacterInteraction.Character => Character;

        protected override void OnStart()
        {
            Character.ColliderInteractions.Add(new ColliderListener<SceneInteractiveElement>(TriggerEnter, TriggerExit));
        }

        protected virtual void TriggerEnter(SceneInteractiveElement obj)
        {
            if (Enter(obj))
                obj.ColiderEnter(this);
        }

        protected virtual void TriggerExit(SceneInteractiveElement obj)
        {
            if (Exit(obj))
                obj.ColiderExit(this);
        }

        private bool Enter(SceneInteractiveElement obj)
        {
            bool report = false;
            foreach (var interaction in _definitions)
                report |= interaction.Enter(obj, this);
            return report;
        }

        private bool Exit(SceneInteractiveElement obj)
        {
            bool report = false;
            foreach (var interaction in _definitions)
                report |= interaction.Exit(obj, this);
            return report;
        }
    }
}