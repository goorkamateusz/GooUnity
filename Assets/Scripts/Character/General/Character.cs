using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _identifier;
    [SerializeField] private GameObject[] _abilityNodes;
    [SerializeField] private GameObject[] _compontentNodes;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private AnimatorHandler _animatorHandler;
    [SerializeField] private CharacterInput _characterInput;
    [SerializeField] private CharacterColliderInteractions _colliderInteractions;

    public string Id => _identifier;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public CharacterMovement Movement => _movement;
    public CharacterInput Input => _characterInput;
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
        InitializeComponents<Ability>(_compontentNodes);
        InitializeComponents<Ability>(_abilityNodes);
    }

    private void InitializeComponents()
    {
        throw new NotImplementedException();
    }

    private void InitializeComponents<T>(GameObject[] nodes) where T : ICharacterComponent
    {
        foreach (var node in nodes)
        {
            T[] abilities = node.GetComponents<T>();
            foreach (var ability in abilities)
            {
                ability.InjectCharacter(this);
            }
        }
    }
}
