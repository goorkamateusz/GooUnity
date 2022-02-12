using Assets.Goo.SceneObjects;
using UnityEngine;

public abstract class Teleport : SceneInteractiveElement
{
    private bool _disabled = false;

    public override void ColiderEnter(ICharacterInteraction character)
    {
        if (_disabled) return;
        if (ValidateCharacter(character))
        {
            TeleportCharacterOnEnter(character);
        }
    }

    public override void ColiderExit(ICharacterInteraction character)
    {
        _disabled = false;
        HideTip(character);
    }

    protected static void Move(ICharacterInteraction character, Transform target)
    {
        character.Character.Movement.Wrap(target);
        if (character.IsPlayer)
            character.Character.Movement.Stop();


        var targetTeleport = target.GetComponent<Teleport>();
        if (targetTeleport != null)
            targetTeleport._disabled = true;
    }

    protected virtual bool ValidateCharacter(ICharacterInteraction character)
    {
        return character.IsPlayer;
    }

    protected abstract void TeleportCharacterOnEnter(ICharacterInteraction character);
}
