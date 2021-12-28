using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _abilityNodes;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private AnimatorHandler _animatorHandler;

    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public PlayerMovement Movement => _movement;

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
        _movement?.InjectPlayer(this);
        InitializeAbilities();
    }

    private void InitializeAbilities()
    {
        foreach (var node in _abilityNodes)
        {
            Ability[] abilities = node.GetComponents<Ability>();
            foreach (var ability in abilities)
            {
                ability.InjectPlayer(this);
            }
        }
    }
}
