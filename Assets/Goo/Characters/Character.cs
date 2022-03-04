using Assets.Goo.Characters.Interactions;
using UnityEngine;

namespace Assets.Goo.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string _identifier;
        [SerializeField] private CharacterComponentsManager _components = new CharacterComponentsManager();
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private AnimatorHandler _animatorHandler;
        [SerializeField] private CharacterColliderInteractions _colliderInteractions;

        public string Id => _identifier;
        public AnimatorHandler AnimatorHandler => _animatorHandler;
        public CharacterMovement Movement => _movement;
        public CharacterColliderInteractions ColliderInteractions => _colliderInteractions;

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
            _movement?.InjectCharacter(this);
            _components.InitializeComponents(this);
        }

        [ContextMenu("Find components")]
        protected virtual void FindComponents() => _components.FindComponents(transform);

        protected virtual void Reset()
        {
            FindComponents();
        }
    }
}