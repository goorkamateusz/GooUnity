/// It's only animations...
public class JumpAbility : KeyInputOrientedAbility
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnKeyDown()
    {
        Character.AnimatorHandler.SetTrigger("Jump");
    }

    protected override void OnKeyUp()
    {
    }
}
