using System.Collections;
using Assets.Goo.Characters.Ability;
using UnityEngine;

public class DashAbility : KeyInputOrientedAbility
{
    private const string AnimationState = "Squat";
    [SerializeField] private float _distance = 6f;
    [SerializeField] private float _durationTime = 0.2f;
    [SerializeField] private bool _stopMovingAfterDash = true;

    [Header("Visuals")]
    [SerializeField] private ParticleSystem _particle;

    private Coroutine _coroutine;
    private Camera _main;

    protected override void OnKeyDown()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Dash());
    }

    protected override void OnKeyUp()
    {
    }

    protected override void OnStart()
    {
        base.OnStart();
        _main = Camera.main;
    }

    protected virtual IEnumerator Dash()
    {
        Character.Movement.IsEnabled = false;
        Character.AnimatorHandler.SetBool(AnimationState, true);
        _particle.Play();

        yield return DashMovement();

        Character.Movement.IsEnabled = true;
        Character.AnimatorHandler.SetBool(AnimationState, false);
        _particle.Stop();

        if (_stopMovingAfterDash)
            Character.Movement.Stop();

        _coroutine = null;
    }

    private IEnumerator DashMovement()
    {
        Vector3? direction = GetDirection();
        if (direction is null) yield break;

        float timer = 0;
        Vector3 startPoint = Character.Position;
        Vector3 endPoint = startPoint + direction.Value * _distance;

        // todo change logic of obstacles detection
        var agent = Character.Movement.Pathfinding.Agent;

        while (timer < _durationTime)
        {
            timer += Time.deltaTime;
            endPoint.y = startPoint.y;
            Vector3 move = Vector3.Lerp(startPoint, endPoint, timer / _durationTime);
            Character.transform.LookAt(move);

            if (!agent.Raycast(move, out var hit))
            {
                Character.Position = move;
                Debug.DrawLine(Character.Position, move, Color.blue, 0.5f);
            }
            else
            {
                Debug.DrawLine(Character.Position, hit.position, Color.red, 1f);
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
