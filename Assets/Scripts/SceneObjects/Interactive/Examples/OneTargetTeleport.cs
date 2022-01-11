using UnityEngine;

public class OneTargetTeleport : Teleport
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected bool _withoutKey;
    [SerializeField] protected bool _forAi;
    [SerializeField, TextArea] protected string _message = "Use teleport";

    public override void ColiderEnter(ICharacterInteractiveComponent character)
    {
        base.ColiderEnter(character);
        DisplayTip(character, _message);
    }

    public override void OnKeyDown(ICharacterInteractiveComponent character)
    {
        Move(character, _target);
    }

    public override void OnKeyUp(ICharacterInteractiveComponent character)
    {
    }

    protected override bool ValidateCharacter(ICharacterInteractiveComponent character)
    {
        return base.ValidateCharacter(character) || _forAi;
    }

    protected override void TeleportCharacterOnEnter(ICharacterInteractiveComponent character)
    {
        if (_withoutKey || !character.IsPlayer)
            Move(character, _target);
    }
}
