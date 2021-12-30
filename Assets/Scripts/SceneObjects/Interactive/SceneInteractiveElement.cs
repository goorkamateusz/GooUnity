using UnityEngine;

public interface IPlayerInteractiveComponent
{
    public KeyCode Key { get; }
}

[RequireComponent(typeof(Collider))]
public abstract class SceneInteractiveElement : MonoBehaviour
{
    public abstract void ColiderEnter(IPlayerInteractiveComponent player);
    public abstract void ColiderExit(IPlayerInteractiveComponent player);

    public abstract void OnKeyDown(IPlayerInteractiveComponent player);
    public abstract void OnKeyUp(IPlayerInteractiveComponent player);

    protected virtual void DisplayTip(IPlayerInteractiveComponent player)
    {
        DisplayTip(player, string.Empty);
    }

    protected void DisplayTip(IPlayerInteractiveComponent player, string msg)
    {
        if (UiReferenceManager.Initialized)
            UiReferenceManager.Instance?.KeyActionView.DisplayTip(player.Key, msg);
    }

    protected void HideTip(IPlayerInteractiveComponent player)
    {
        if (UiReferenceManager.Initialized)
            UiReferenceManager.Instance.KeyActionView.HideTip(player.Key);
    }
}
