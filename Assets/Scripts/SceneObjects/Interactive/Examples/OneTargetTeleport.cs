using UnityEngine;

public class OneTargetTeleport : Teleport
{
    [SerializeField] protected Teleport _target;
    [SerializeField] protected bool _withoutKey;
    [SerializeField] protected bool _forAi;

    public override void ColiderEnter(IPlayerInteractiveComponent player)
    {
        base.ColiderEnter(player);
        DisplayTip(player, "Use teleport");
    }

    public override void OnKeyDown(IPlayerInteractiveComponent player)
    {
        if (!_withoutKey)
            _target.Move(player);
    }

    public override void OnKeyUp(IPlayerInteractiveComponent player)
    {
    }

    protected override bool ValidatePlayer(IPlayerInteractiveComponent player)
    {
        return base.ValidatePlayer(player); // || _forAi;
    }

    protected override void TeleportPlayerOnEnter(IPlayerInteractiveComponent player)
    {
        if (_withoutKey || !player.IsPlayer)
            _target.Move(player);
    }
}
