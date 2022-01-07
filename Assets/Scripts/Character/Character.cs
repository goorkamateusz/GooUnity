using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _identifier;
    [SerializeField] private GameObject[] _abilityNodes;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private AnimatorHandler _animatorHandler;
    [SerializeField] private CharacterInput _characterInput;

    public string Id => _identifier;
    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public CharacterMovement Movement => _movement;
    public CharacterInput Input => _characterInput;

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
        InitializeAbilities();
    }

    private void InitializeAbilities()
    {
        foreach (var node in _abilityNodes)
        {
            Ability[] abilities = node.GetComponents<Ability>();
            foreach (var ability in abilities)
            {
                ability.InjectCharacter(this);
            }
        }
    }
}
