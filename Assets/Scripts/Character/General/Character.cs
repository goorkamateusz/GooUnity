using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _identifier;
    [SerializeField] private GameObject[] _compontentNodes;
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
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        foreach (var node in _compontentNodes)
        {
            var abilities = node.GetComponents<ICharacterComponent>();
            foreach (var ability in abilities)
            {
                ability.InjectCharacter(this);
            }
        }
    }
}
