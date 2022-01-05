public abstract class Teleport : SceneInteractiveElement
{
    private bool _disabled = false;

    public override void ColiderEnter(IPlayerInteractiveComponent player)
    {
        if (_disabled) return;
        if (ValidatePlayer(player))
        {
            TeleportPlayerOnEnter(player);
        }
    }

    public override void ColiderExit(IPlayerInteractiveComponent player)
    {
        _disabled = false;
    }

    public void Move(IPlayerInteractiveComponent character)
    {
        character.Character.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _disabled = true;
        // idea visuals
    }

    protected virtual bool ValidatePlayer(IPlayerInteractiveComponent player)
    {
        return player.IsPlayer;
    }

    protected abstract void TeleportPlayerOnEnter(IPlayerInteractiveComponent player);
}
