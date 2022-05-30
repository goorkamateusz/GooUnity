using UnityEngine;
using Goo.Characters.Interactions;

namespace Goo.Characters
{
    public class Character : MonoBehaviour
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public CharacterComponentsManager Components { get; private set; } = new CharacterComponentsManager();
        [field: SerializeField] public CharacterMovement Movement { get; private set; }
        [field: SerializeField] public AnimatorHandler AnimatorHandler { get; private set; }
        [field: SerializeField] public CharacterColliderInteractions ColliderInteractions { get; private set; }

        // todo create sth like ITransform to manage all character transform in one replaceable object
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        private void Awake()
        {
            Movement?.InjectCharacter(this);
            Components.InitializeComponents(this);
        }

#if UNITY_EDITOR
        [ContextMenu("Find components")]
        protected virtual void __FindComponents() => Components.__FindComponents(transform);

        protected virtual void Reset()
        {
            __FindComponents();
        }
#endif
    }
}