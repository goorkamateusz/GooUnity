public class StopMovingAbility : KeyInputOrientedAbility
{
    protected override void OnKeyDown()
    {
        Character.Movement.Stop();
    }

    protected override void OnKeyUp()
    {
    }
}
