using UnityEngine;
using Goo.Tools.EventSystem;

public class PrinterEvent : IEvent<PrinterEvent>
{
    public string Text;
}

public class EventPrinterListener : MonoBehaviour, IEventListener<PrinterEvent>
{
    protected void Awake()
    {
        this.SubscribeEvent();
    }

    protected void OnDestroy()
    {
        this.UnsubscribeEvent();
    }

    public void OnEvent(PrinterEvent e)
    {
        Debug.Log(e.Text);
    }
}
