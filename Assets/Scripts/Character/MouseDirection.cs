using UnityEngine;

public class MouseDirection : DirectionVisual
{
    private Camera _main;

    private void Awake()
    {
        _main = Camera.main;
    }

    protected override Quaternion? GetRotation()
    {
        var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
        {
            Vector3 point = hit.point;
            point.y = transform.position.y;
            return Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
        }
        return null;
    }
}
