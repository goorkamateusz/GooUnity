using UnityEngine;

namespace Assets.Goo.SceneObjects.Characters
{
    public abstract class AutoSceneInteractionDefinitions : MonoBehaviour
    {
        public abstract bool Enter(SceneInteractiveElement obj, AutoSceneInteractionAbility ability);
        public abstract bool Exit(SceneInteractiveElement obj, AutoSceneInteractionAbility ability);
    }

    public abstract class AutoSceneInteractionDefinitions<T> : AutoSceneInteractionDefinitions where T : SceneInteractiveElement
    {
        public override bool Enter(SceneInteractiveElement obj, AutoSceneInteractionAbility ability)
        {
            T element = obj as T;
            if (element != null)
                return OnEnter(element, ability);
            return false;
        }

        public override bool Exit(SceneInteractiveElement obj, AutoSceneInteractionAbility ability)
        {
            T element = obj as T;
            if (element != null)
                return OnExit(element, ability);
            return false;
        }

        protected abstract bool OnEnter(T obj, AutoSceneInteractionAbility ability);
        protected abstract bool OnExit(T obj, AutoSceneInteractionAbility ability);
    }
}