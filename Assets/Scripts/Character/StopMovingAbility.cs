public class StopMovingAbility : KeyInputOrientedAbility
{
    protected override void OnKeyDown()
    {
        Player.Movement.Stop();
    }

    protected override void OnKeyUp()
    {
    }
}
