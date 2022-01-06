using UnityEngine;

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
        HideTip(character);
    }

    protected static void Move(ICharacterInteractiveComponent character, Transform target)
    {
        character.Character.transform.SetPositionAndRotation(target.position, target.rotation);
        var targetTeleport = target.GetComponent<Teleport>();
        if (targetTeleport != null)
            targetTeleport._disabled = true;
    }

    protected virtual bool ValidateCharacter(ICharacterInteractiveComponent character)
    {
        return character.IsCharacter;
    }

    protected abstract void TeleportCharacterOnEnter(ICharacterInteractiveComponent character);
}