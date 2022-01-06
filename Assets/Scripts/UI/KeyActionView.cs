using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct KeyTipData
{
    public KeyCode key;
    public string desc;
    public float lifeTime;
}

public class KeyActionView : MonoBehaviour
{
    [SerializeField] private List<KeyActionTipView> _views;

    private Queue<KeyTipData> _queue;

    public void DisplayTip(KeyCode key, string desc, float lifeTime = 3f)
    {
        var view = GetInactive();
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

    private KeyActionTipView GetInactive()
    {
        return _views.FirstOrDefault((view) => !view.IsActive);
    }

    private KeyActionTipView GetView(KeyCode key)
    {
        return _views.FirstOrDefault((view) => view.Key == key);
    }
}
