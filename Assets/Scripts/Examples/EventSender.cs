using Goo.Tools.EventSystem;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    private PrinterEvent printerEvent = new PrinterEvent
    {
        Text = $"Pressed {KeyCode.P}"
    };

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            printerEvent.Send();
        }
    }
}
