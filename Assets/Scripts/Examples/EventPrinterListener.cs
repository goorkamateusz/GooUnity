using UnityEngine;
using Assets.Goo.Tools.EventSystem;

public class PrinterEvent
{
    public string Text;
}

public class EventPrinterListener : MonoBehaviour, IEventListener<PrinterEvent>
{
    protected void Awake()
    {
        EventManager.Instance.Subscribe(this);
    }

    protected void OnDestory()
    {
        EventManager.Instance.Unsubscribe(this);
    }

    public void OnTrigger(PrinterEvent e)
    {
        Debug.Log(e.Text);
    }
}
