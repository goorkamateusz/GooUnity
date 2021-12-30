using UnityEngine;

public class RunAbility : KeyInputOrientedAbility
{
    [Header("Run Ability")]
    [SerializeField] private float _speedMultiplier;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnKeyDown()
    {
        Player.Movement.SetMultiplier(_speedMultiplier);
    }

    protected override void OnKeyUp()
    {
        Player.Movement.ResetMultiplier();
    }
}
