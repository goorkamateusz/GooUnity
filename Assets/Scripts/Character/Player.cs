using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _abilityNodes;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private AnimatorHandler _animatorHandler;

    public AnimatorHandler AnimatorHandler => _animatorHandler;
    public PlayerMovement Movement => _movement;

    private void Awake()
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
