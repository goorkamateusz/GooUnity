using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class NpcPath : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    public Transform this[int index] => _points[index];

    public int Count => _points.Count;

    public int NextPointIndex(int currentPoint)
    {
        return (currentPoint + 1) % Count;
    }

    protected virtual void Reset()
    {
        var points = transform.GetComponentsInChildren<Transform>();
        _points = points.Where(point => point != transform).ToList();
    }
}
