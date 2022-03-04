using Assets.Goo.Characters;
using Assets.Goo.Characters.Ability;
using Assets.Goo.Characters.Interactions;
using Assets.Goo.SceneObjects;
using UnityEngine;

public class MultipleSceneInteractionAbility : Ability
{
    private struct CharacterInteractiveDto : ICharacterInteraction
    {
        public MultipleSceneInteractionAbility Parent { get; set; }
        public KeyCode Key { get; set; }

        public bool IsPlayer => Parent.IsCharacter;
        public Character Character => Parent.Character;
        public bool DisplayUI => true;
    }

    [SerializeField] private CharacterColliderInteractions _interactions;
    [SerializeField] private KeyCode[] _actionsKeys;

    private SceneInteractiveElement[] _objects;
    private CharacterInteractiveDto[] _interactivesDtos;

    public virtual bool IsCharacter => true;

    private int Length => _actionsKeys.Length;

    protected void Awake()
    {
        Initialize();
    }

    protected override void OnStart()
    {
        base.OnStart();
        _interactions.Add(new ColliderListener<SceneInteractiveElement>(TriggerEnter, TriggerExit));
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void TriggerEnter(SceneInteractiveElement obj)
    {
        if (TryGetEmptySlot(out int index))
        {
            _objects[index] = obj;
            obj.ColiderEnter(_interactivesDtos[index]);
        }
    }

    protected virtual void TriggerExit(SceneInteractiveElement obj)
    {
        if (TryGetSlot(obj, out int index))
        {
            _objects[index] = null;
            obj.ColiderExit(_interactivesDtos[index]);
        }
    }

    protected virtual void HandleInput()
    {
        // todo move to Char. Input
        for (int i = 0; i < _actionsKeys.Length; i++)
        {
            if (Input.GetKeyDown(_actionsKeys[i]))
            {
                _objects[i]?.OnKeyDown(_interactivesDtos[i]);
            }

            if (Input.GetKeyUp(_actionsKeys[i]))
            {
                _objects[i]?.OnKeyUp(_interactivesDtos[i]);
            }
        }
    }

    private bool TryGetEmptySlot(out int index)
    {
        return TryGetSlot(null, out index);
    }

    private bool TryGetSlot(SceneInteractiveElement obj, out int index)
    {
        for (int i = 0; i < _actionsKeys.Length; i++)
        {
            if (_objects[i] == obj)
            {
                index = i;
                return true;
            }
        }
        index = 0;
        return false;
    }

    private void Initialize()
    {
        _interactivesDtos = new CharacterInteractiveDto[Length];
        _objects = new SceneInteractiveElement[Length];

        for (int i = 0; i < Length; i++)
        {
            _interactivesDtos[i].Key = _actionsKeys[i];
            _interactivesDtos[i].Parent = this;
        }
    }
}
