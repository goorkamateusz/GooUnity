public class DamageAnimateTest : KeyInputOrientedAbility
{
    private int _damageId;

    protected override void OnKeyDown()
    {
        Player.AnimatorHandler.SetTrigger("Damage");
        Player.AnimatorHandler.SetInt("DamageID", _damageId);
        _damageId = (_damageId + 1) % 3;
    }

    protected override void OnKeyUp()
    {
    }
}
