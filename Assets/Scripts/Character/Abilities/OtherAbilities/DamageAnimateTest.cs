using Assets.Goo.Characters.Ability;

public class DamageAnimateTest : KeyInputOrientedAbility
{
    private int _damageId;

    protected override void OnKeyDown()
    {
        Character.AnimatorHandler.SetTrigger("Damage");
        Character.AnimatorHandler.SetInt("DamageID", _damageId);
        _damageId = (_damageId + 1) % 3;
    }

    protected override void OnKeyUp()
    {
    }
}
