using UnityEngine;

public class Ability : MonoBehaviour
{
    protected Player Player { get; private set; }

    public void InjectPlayer(Player player)
    {
        Player = player;
    }
}

public abstract class InputOrientedAbility : Ability
{
    protected abstract void HandleInput();

    protected virtual void Start() { }

    protected virtual void Update()
    {
        HandleInput();
    }
}

public abstract class KeyInputOrientedAbility : InputOrientedAbility
{
    [SerializeField] protected KeyCode _key;

    protected override void HandleInput()
    {
        if (Input.GetKeyDown(_key))
        {
            OnKeyDown();
        }
        if (Input.GetKeyUp(_key))
        {
            OnKeyUp();
        }
    }

    protected abstract void OnKeyDown();
    protected abstract void OnKeyUp();
}
