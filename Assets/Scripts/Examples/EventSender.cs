using Assets.Goo.Tools.EventSystem;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.Instance.Trigger(new PrinterEvent
            {
                Text = $"Pressed {KeyCode.P}"
            });
        }
    }
}
