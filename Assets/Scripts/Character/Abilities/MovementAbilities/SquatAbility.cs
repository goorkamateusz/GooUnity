using UnityEngine;

public class SquatAbility : KeyInputOrientedAbility
{
    [SerializeField] private float _movementMultiplier = 0.8f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnKeyDown()
    {
        Player.AnimatorHandler.SetBool("Squat", true);
        Player.Movement.SetMultiplier(_movementMultiplier);
    }

    protected override void OnKeyUp()
    {
        Player.AnimatorHandler.SetBool("Squat", false);
        Player.Movement.ResetMultiplier();
    }
}
