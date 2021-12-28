using System;
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

        yield return DashMovement();

        Player.Movement.IsEnabled = true;

        if (_stopMovingAfterDash)
            Player.Movement.Stop();
    }

    private IEnumerator DashMovement()
    {
        Vector3? direction = GetDirection();
        if (direction is null) yield break;

        float timer = 0;
        Vector3 startPoint = Player.Position;
        Vector3 endPoint = startPoint + direction.Value * _distance;

        Debug.DrawLine(startPoint, endPoint, Color.black, 1f);

        var agent = Player.Movement.Agent;

        while (timer < _durationTime)
        {
            timer += Time.deltaTime;
            endPoint.y = startPoint.y;
            Vector3 move = Vector3.Lerp(startPoint, endPoint, timer / _durationTime);

            if (agent.Raycast(move, out var hit))
            {
                Debug.DrawLine(Player.Position, hit.position, Color.blue, 0.2f);
            }
            else
            {
                Player.Position = move;
                Debug.DrawLine(Player.Position, move, Color.red, 0.1f);
            }

            yield return null;
        }
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
