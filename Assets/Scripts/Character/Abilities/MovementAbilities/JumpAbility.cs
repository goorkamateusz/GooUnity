/// It's only animations...
public class JumpAbility : KeyInputOrientedAbility
{
    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnKeyDown()
    {
        Character.AnimatorHandler.SetTrigger("Jump");
    }

    protected override void OnKeyUp()
    {
    }
}
