using UnityEngine;

public class MessageBox : SceneInteractiveElement
{
    [SerializeField, TextArea] private string _message;

    public override void ColiderEnter(IPlayerInteractiveComponent player)
    {
        Debug.Log("MessageBox");
        if (player.IsPlayer && UiReferenceManager.Initialized)
            UiReferenceManager.Instance.MessagePopup.Open(_message);
    }

    public override void ColiderExit(IPlayerInteractiveComponent player)
    {
    }

    public override void OnKeyDown(IPlayerInteractiveComponent player)
    {
    }

    public override void OnKeyUp(IPlayerInteractiveComponent player)
    {
    }
}
