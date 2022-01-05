public abstract class Teleport : SceneInteractiveElement
{
    private bool _disabled = false;

    public override void ColiderEnter(ICharacterInteractiveComponent character)
    {
        if (_disabled) return;
        if (ValidateCharacter(character))
        {
            TeleportCharacterOnEnter(character);
        }
    }

    public override void ColiderExit(ICharacterInteractiveComponent character)
    {
        _disabled = false;
    }

    public void Move(ICharacterInteractiveComponent character)
    {
        character.Character.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _disabled = true;
        // idea visuals
    }

    protected virtual bool ValidateCharacter(ICharacterInteractiveComponent character)
    {
        return character.IsCharacter;
    }

    protected abstract void TeleportCharacterOnEnter(ICharacterInteractiveComponent character);
}
