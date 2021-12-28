public class DashAbility : KeyInputOrientedAbility
{
    protected override void OnKeyDown()
    {
        Player.Movement.IsEnabled = false;
    }

    protected override void OnKeyUp()
    {
        Player.Movement.IsEnabled = true;
    }
}
