using System.Collections;
using UnityEngine;

public class DashAbility : KeyInputOrientedAbility
{
    [SerializeField] private float _distance = 6f;
    [SerializeField] private float _durationTime = 0.2f;
    [SerializeField] private bool _stopMovingAfterDash = true;

    private Coroutine _coroutine;
    private Camera _main;

    protected override void OnKeyDown()
    {
        _coroutine = StartCoroutine(Dash());
    }

    protected override void OnKeyUp()
    {
    }

    protected override void Start()
    {
        base.Start();
        _main = Camera.main;
    }

    protected virtual IEnumerator Dash()
    {
        Player.Movement.IsEnabled = false;

        Vector3? direction = GetDirection();
        if (direction is null) yield break;

        float timer = 0;
        Vector3 startPoint = Player.Position;
        Vector3 endPoint = startPoint + direction.Value * _distance;

        while (timer < _durationTime)
        {
            timer += Time.deltaTime;
            endPoint.y = Player.Position.y;
            Player.Position = Vector3.Lerp(startPoint, endPoint, timer / _durationTime);
            yield return null;
        }

        Player.Movement.IsEnabled = true;

        if (_stopMovingAfterDash)
            Player.Movement.Stop();
    }

    private Vector3? GetDirection()
    {
        var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
        {
            Vector3 point = hit.point;
            point.y = transform.position.y;
            return (hit.point - transform.position).normalized;
        }
        return null;
    }
}
