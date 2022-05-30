using Goo.Characters.Ability;
using UnityEngine;

public class SquatAbility : KeyInputOrientedAbility
{
    [SerializeField] private float _movementMultiplier = 0.8f;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnKeyDown()
    {
        Character.AnimatorHandler.SetBool("Squat", true);
        Character.Movement.SetMultiplier(_movementMultiplier);
    }

    protected override void OnKeyUp()
    {
        Character.AnimatorHandler.SetBool("Squat", false);
        Character.Movement.ResetMultiplier();
    }
}
