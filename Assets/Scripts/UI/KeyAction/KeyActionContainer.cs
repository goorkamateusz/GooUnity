using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct KeyTipData
{
    public KeyCode key;
    public string desc;
    public float? lifeTime;
}

public class KeyActionContainer : MonoBehaviour
{
    [SerializeField] private List<KeyActionView> _views;

    private Queue<KeyTipData> _queue = new Queue<KeyTipData>();

    public void DisplayTip(KeyCode key, string desc)
    {
        DisplayTip(key, desc, null);
    }

    public void DisplayTip(KeyCode key, string desc, float lifeTime)
    {
        DisplayTip(key, desc, lifeTime);
    }

    private void DisplayTip(KeyCode key, string desc, float? lifeTime)
    {
        var view = GetView(key) ?? GetInactive();
        if (view is null)
        {
            _queue.Enqueue(new KeyTipData
            {
                key = key,
                desc = desc,
                lifeTime = lifeTime
            });
        }
        else
        {
            view.DisplayTip(key, desc, lifeTime);
        }
    }

    public void HideTip(KeyCode key)
    {
        var view = GetView(key);
        if (view != null)
        {
            view.HideTip();
        }
    }

    private int EmptySlots()
    {
        return _views.Count - _views.Count((view) => view.IsActive);
    }

    private KeyActionView GetInactive()
    {
        return _views.FirstOrDefault((view) => !view.IsActive);
    }

    private KeyActionView GetView(KeyCode key)
    {
        return _views.FirstOrDefault((view) => view.Key == key);
    }
}
