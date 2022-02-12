using Assets.Goo.SceneObjects;
using UnityEngine;

public class OneTargetTeleport : Teleport
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected bool _withoutKey;
    [SerializeField] protected bool _forAi;
    [SerializeField, TextArea] protected string _message = "Use teleport";

    public override void ColiderEnter(ICharacterInteraction character)
    {
        base.ColiderEnter(character);
        DisplayTip(character, _message);
    }

    public override void OnKeyDown(ICharacterInteraction character)
    {
        Move(character, _target);
    }

    public override void OnKeyUp(ICharacterInteraction character)
    {
    }

    protected override bool ValidateCharacter(ICharacterInteraction character)
    {
        return base.ValidateCharacter(character) || _forAi;
    }

    protected override void TeleportCharacterOnEnter(ICharacterInteraction character)
    {
        if (_withoutKey || !character.IsPlayer)
            Move(character, _target);
    }
}
