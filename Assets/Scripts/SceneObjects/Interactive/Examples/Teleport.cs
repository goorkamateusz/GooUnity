using System;
using UnityEngine;
using Goo.Characters;
using Goo.SceneObjects;

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
        throw new NotImplementedException();
    }

    protected virtual bool ValidateCharacter(ICharacterInteraction character)
    {
        return character.Character is Player;
    }

    protected abstract void TeleportCharacterOnEnter(ICharacterInteraction character);
}
