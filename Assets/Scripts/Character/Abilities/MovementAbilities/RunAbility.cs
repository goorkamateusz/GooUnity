using Goo.Characters.Ability;
using UnityEngine;

public class RunAbility : KeyInputOrientedAbility
{
    [Header("Run Ability")]
    [SerializeField] private float _speedMultiplier;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnKeyDown()
    {
        Character.Movement.SetMultiplier(_speedMultiplier);
    }

    protected override void OnKeyUp()
    {
        Character.Movement.ResetMultiplier();
    }
}
